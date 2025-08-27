import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';
import '../CSS/BookForm.css';

const API_URL = 'http://localhost:5231/api/book';
const GENRE_URL = 'http://localhost:5231/api/genre';
const PUBLISHER_URL = 'http://localhost:5231/api/publisher';

const BookFormPage = () => {
    const [book, setBook] = useState({
        title: '',
        author: '',
        isbn: '',
        genreId: '',
        publisherId: '',
        publishedYear: '',
        quantity: '',
        price: '',
    });
    const [genres, setGenres] = useState([]);
    const [publishers, setPublishers] = useState([]);
    const [isEdit, setIsEdit] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        fetchGenres();
        fetchPublishers();
        if (id) {
            setIsEdit(true);
            fetchBook(id);
        }
    }, [id]);

    const fetchGenres = async () => {
        try {
            const response = await fetch(GENRE_URL);
            const data = await response.json();
            setGenres(data);
        } catch (error) {
            console.error("Error fetching genres:", error);
        }
    };

    const fetchPublishers = async () => {
        try {
            const response = await fetch(PUBLISHER_URL);
            const data = await response.json();
            setPublishers(data);
        } catch (error) {
            console.error("Error fetching publishers:", error);
        }
    };

    const fetchBook = async (id) => {
        try {
            const response = await fetch(`${API_URL}/${id}`, {
                headers: getAuthHeaders(),
            });
            const data = await response.json();
            setBook(data);
        } catch (error) {
            console.error("Error fetching book:", error);
            setError("Failed to fetch book.");
        }
    };

    const handleChange = (e) => {
        setBook({ ...book, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (isEdit) {
                await fetch(`${API_URL}/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(book),
                });
            } else {
                await fetch(API_URL, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(book),
                });
            }
            navigate('/admin/book-list');
        } catch (error) {
            console.error("Error saving book:", error);
            setError("Failed to save book.");
        }
    };

    return (
        <div className="book-form-page-container">
            <h2>{isEdit ? 'Update Book' : 'Add Book'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="input-group">
                    <label>Title:</label>
                    <input
                        type="text"
                        name="title"
                        value={book.title}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Author:</label>
                    <input
                        type="text"
                        name="author"
                        value={book.author}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>ISBN:</label>
                    <input
                        type="text"
                        name="isbn"
                        value={book.isbn}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Genre:</label>
                    <select
                        name="genreId"
                        value={book.genreId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select Genre</option>
                        {genres.map((genre) => (
                            <option key={genre.genreId} value={genre.genreId}>
                                {genre.genreName}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="input-group">
                    <label>Publisher:</label>
                    <select
                        name="publisherId"
                        value={book.publisherId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Select Publisher</option>
                        {publishers.map((publisher) => (
                            <option key={publisher.publisherId} value={publisher.publisherId}>
                                {publisher.publisherName}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="input-group">
                    <label>Published Year:</label>
                    <input
                        type="number"
                        name="publishedYear"
                        value={book.publishedYear}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Quantity:</label>
                    <input
                        type="number"
                        name="quantity"
                        value={book.quantity}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Price:</label>
                    <input
                        type="number"
                        name="price"
                        value={book.price}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit" className="submit-button">
                    {isEdit ? 'Update' : 'Add'}
                </button>
            </form>
        </div>
    );
};

export default BookFormPage;