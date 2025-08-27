import React, { useEffect, useState } from 'react';
import Header from '../components/Header';
import { Link } from 'react-router-dom';
import '../components/CSS/HomePage.css'; 

const API_BASE_URL = 'http://localhost:5231';

const HomePage = () => {
  const [newBooks, setNewBooks] = useState([]);
  const [popularBooks, setPopularBooks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const newBooksResponse = await fetch(`${API_BASE_URL}/api/book?sort=newest&limit=8`, {
          headers: {
            'Accept': 'application/json',
          }
        });

        if (!newBooksResponse.ok) {
          throw new Error(`HTTP error! status: ${newBooksResponse.status}`);
        }

        const newBooksData = await newBooksResponse.json();
        setNewBooks(newBooksData);

        const popularBooksResponse = await fetch(`${API_BASE_URL}/api/book?sort=popular&limit=8`, {
          headers: {
            'Accept': 'application/json',
          }
        });

        if (!popularBooksResponse.ok) {
          throw new Error(`HTTP error! status: ${popularBooksResponse.status}`);
        }

        const popularBooksData = await popularBooksResponse.json();
        setPopularBooks(popularBooksData);

        setLoading(false);
      } catch (error) {
        console.error('Error fetching books:', error);
        setError(error.message);
        setLoading(false);
      }
    };

    fetchBooks();
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <div>
      <Header />
      <div className="homepage-container">
        <h2 className="section-title">Newest Books</h2>
        <div className="books-grid">
          {newBooks.map(book => (
            <div key={book.bookId} className="book-card">
              <h5 className="book-title">{book.title}</h5>
              <p className="book-author">{book.author}</p>
              <p className="book-price">Price: {book.price} VND</p>
              <Link to={`/book/${book.bookId}`} className="details-button">Details</Link>
            </div>
          ))}
        </div>
        <br/>

        <h2 className="section-title">Popular Books</h2>
        <div className="books-grid">
          {popularBooks.map(book => (
            <div key={book.bookId} className="book-card">
              <h5 className="book-title">{book.title}</h5>
              <p className="book-author">{book.author}</p>
              <p className="book-price">Price: {book.price} VND</p>
              <Link to={`/book/${book.bookId}`} className="details-button">Details</Link>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default HomePage;