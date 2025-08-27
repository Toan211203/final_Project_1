import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Header from '../../components/Header';
import '../../components/CSS/BookDetail.css';
import { getUserIdFromToken } from '../../services/userAuth';

const API_BASE_URL = 'http://localhost:5231';

const BookDetails = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [quantity, setQuantity] = useState(1);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
      const fetchBook = async () => {
          try {
              const response = await fetch(`${API_BASE_URL}/api/book/${id}`);
              
              if (!response.ok) {
                  throw new Error(`HTTP error! Status: ${response.status}`);
              }

              const bookData = await response.json();
              setBook(bookData);
              setLoading(false);

              const reviewsResponse = await fetch(`${API_BASE_URL}/api/review/book/${bookData.bookId}`);
              if (reviewsResponse.ok) {
                  const reviewsData = await reviewsResponse.json();
                  setBook(prevBook => ({
                      ...prevBook,
                      Reviews: reviewsData
                  }));
              }
          } catch (error) {
              console.error('Error fetching book:', error);
              setError(error.message);
              setLoading(false);
          }
      };

      fetchBook();
  }, [id]);

  const renderStars = (rating) => {
          const stars = [];
          for (let i = 0; i < 5; i++) {
              stars.push(
                  <span key={i} className={i < rating ? 'star filled' : 'star'}>★</span>
              );
          }
          return stars;
      };

    const handleAddToCart = async () => {
        const userId = getUserIdFromToken();
        if (!userId) {
            alert('Please log in before adding to the cart.');
            return;
        }

        const payload = {
            userId: userId,
            bookId: book.bookId,
            quantity: parseInt(quantity),
        };

        try {
            const response = await fetch(`${API_BASE_URL}/api/cart`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                alert('Added to cart successfully!');
            } else {
                const err = await response.json();
                alert(`Error adding to cart: ${err.message || response.statusText}`);
            }
        } catch (err) {
            console.error('API call error:', err);
            alert('An error occurred, please try again later.');
        }
    };

    const handleRateBook = () => {
        navigate(`/review-book`, { state: { bookId: book.bookId, bookTitle: book.title } });
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!book) return <div>No book found.</div>;

    return (
        <div className="book-detail-container">
          <Header />
          <div className="book-info">
              <h2>{book.title}</h2>
              <p><strong>Author:</strong> {book.author}</p>
              <p><strong>Publisher:</strong> {book.publisherName}</p>
              <p><strong>Genre:</strong> {book.genreName}</p>
              <p><strong>ISBN:</strong> {book.isbn}</p>
              <p><strong>Published Year:</strong> {book.publishedYear}</p>
              <p><strong>Quantity:</strong> {book.quantity}</p>
              <p><strong>Price:</strong> {book.price} VND</p>
              <div className="mb-3">
                  <label htmlFor="quantity" className="form-label">Select Quantity:</label>
                  <input
                      type="number"
                      id="quantity"
                      className="form-control"
                      value={quantity}
                      onChange={(e) => setQuantity(e.target.value)}
                      min="1"
                      max={book.quantity}
                  />
              </div>
              <button className="btn btn-primary" onClick={handleAddToCart}>
                  Add to Cart
              </button>
              <button className="btn btn-secondary ms-2" onClick={handleRateBook}>
                  Rate Book
              </button>
          </div>

          <div className="review-section mt-5">
              <h3>Reviews</h3>
              {book.Reviews && book.Reviews.length > 0 ? (
                  <div className="review-list">
                      {book.Reviews.map((review) => (
                          <div key={review.reviewId} className="review-card">
                              <p className='review-name'>User: {review.fullName}</p>
                              <p className="review-rating">Rating: {renderStars(review.rating)}</p>
                              <p className="review-comment">{review.comment}</p>
                          </div>
                      ))}
                  </div>
              ) : (
                  <p>No reviews yet.</p>
              )}
          </div>
      </div>
    );
};

export default BookDetails;