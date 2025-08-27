import React, { useEffect, useState } from 'react';
import { getAuthHeaders, getUserIdFromToken } from '../../services/userAuth';
import Headers from '../../components/Header';
import Sidebar from '../../components/Sidebar';

const API_BASE_URL = 'http://localhost:5231';

const Cart = () => {
  const [cartItems, setCartItems] = useState([]);
  const [total, setTotal] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchCartItems = async () => {
      const userId = getUserIdFromToken();
      if (!userId) {
        setError("User could not be identified.");
        setLoading(false);
        return;
      }

      try {
        const response = await fetch(`${API_BASE_URL}/api/cart/user/${userId}`, {
          headers: getAuthHeaders(),
        });

        if (!response.ok) {
          throw new Error(`Error fetching cart: ${response.status}`);
        }

        const data = await response.json();
        setCartItems(data);
        calculateTotal(data);
        setLoading(false);
      } catch (err) {
        setError(err.message);
        setLoading(false);
      }
    };

    fetchCartItems();
  }, []);

  const calculateTotal = (items) => {
    const totalAmount = items.reduce((acc, item) => acc + item.book.price * item.quantity, 0);
    setTotal(totalAmount);
  };

  const handleCheckout = async () => {
    const userId = getUserIdFromToken();
    if (!userId) {
      alert("You are not logged in");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/api/rental`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          ...getAuthHeaders(),
        },
        body: JSON.stringify({ userId }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Error creating rental');
      }

      const rental = await response.json();
      alert('Checkout successful! Rental ID: ' + rental.rentalId);
      setCartItems([]);
      setTotal(0);
    } catch (err) {
      alert('Error: ' + err.message);
    }
  };

  const handleClearCart = async () => {
    const userId = getUserIdFromToken();
    if (!userId) {
      alert("You are not logged in");
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/api/cart/user/${userId}`, {
        method: 'DELETE',
        headers: getAuthHeaders(),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Error clearing cart');
      }

      setCartItems([]);
      setTotal(0);
      alert('Cart cleared successfully!');
    } catch (err) {
      alert('Error: ' + err.message);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <Headers />
      <div style={{ display: 'flex', padding: '1rem', maxWidth: '100%' }}>
        <div style={{ flex: 2 }}>
          <h2>YOUR BOOK CART</h2>
          {cartItems.length === 0 ? (
            <p>Your cart is empty.</p>
          ) : (
            cartItems.map(item => (
              <div key={item.cartId} style={{ display: 'flex', justifyContent: 'space-between', padding: '1rem', borderBottom: '1px solid #ccc' }}>
                <div>
                  <strong>{item.book.title}</strong>
                  <div>Author: {item.book.author}</div>
                </div>
                <div>
                  <div>Quantity: {item.quantity}</div>
                  <div>Price: <span style={{ color: 'red' }}>{item.book.price.toLocaleString()}đ</span></div>
                </div>
              </div>
            ))
          )}
        </div>

        <div style={{ flex: 1, paddingLeft: '2rem', borderLeft: '1px solid #ccc' }}>
          <h3>Total: <span style={{ color: 'blue' }}>{total.toLocaleString()}đ</span></h3>
          <button
            style={{ padding: '0.5rem 1rem', backgroundColor: 'orange', color: 'white', border: 'none', marginTop: '1rem' }}
            onClick={handleCheckout}
          >
            Rent Books
          </button>
          <button
            style={{ padding: '0.5rem 1rem', backgroundColor: 'red', color: 'white', border: 'none', marginTop: '1rem', marginLeft: '1rem' }}
            onClick={handleClearCart}
          >
            Clear Cart
          </button>
        </div>
      </div>
    </>
  );
};

export default Cart;