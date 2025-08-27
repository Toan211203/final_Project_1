import React, { useEffect, useState } from 'react';
import Header from '../components/Header';
import { useLocation, Link } from 'react-router-dom';
import '../components/CSS/SearchResult.css';

const API_BASE_URL = 'http://localhost:5231';

const SearchResults = () => {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const location = useLocation();

    useEffect(() => {
        const fetchBooks = async () => {
            const params = new URLSearchParams(location.search);
            const query = params.get('query');
            const genreId = params.get('genreId');
            let url = `${API_BASE_URL}/api/book`;

            if (query) {
                url += `/search/${query}`;
            } else if (genreId) {
                url += `/genre/${genreId}`;
            }

            try {
                const response = await fetch(url);
                if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
                const data = await response.json();
                setBooks(data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching books:', error);
                setError(error.message);
                setLoading(false);
            }
        };

        fetchBooks();
    }, [location.search]);

    if (loading) return <div>Loading...</div>;

    return (
        <div>
            <Header />
            <div className="search-container">
                <h2 className="text-center">Search Results</h2>
                <div className="search-card-container">
                    {books.length > 0 ? (
                        books.map(book => (
                            <div key={book.bookId} className="search-card">
                                <div className="search-card-body">
                                    <h5 className="search-card-title">{book.title}</h5>
                                    <p className="search-card-author">{book.author}</p>
                                    <p className="search-card-price">Price: {book.price} VND</p>
                                    <Link to={`/book/${book.bookId}`} className="search-btn">Details</Link>
                                </div>
                            </div>
                        ))
                    ) : (
                        <div className="search-no-results">No books found in this genre.</div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default SearchResults;