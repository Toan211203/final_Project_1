import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/BookList.css';

const BookListPage = () => {
    const [books, setBooks] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchBooks();
        }
    }, [navigate]);

    const fetchBooks = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/book');
            const data = await response.json();
            setBooks(data);
        } catch (error) {
            console.error("Error fetching books:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this book? This action cannot be undone.")) {
            try {
                await fetch(`http://localhost:5231/api/book/${id}`, {
                    method: 'DELETE',
                });
                fetchBooks();
            } catch (error) {
                console.error("Error deleting book:", error);
            }
        }
    };

    return (
        <>
            <div className="book-list-page-container">
                <h2>Book List</h2>
                <Link to="/admin/book-form" className="book-list-page-add-button">Add Book</Link>
                <table className="book-list-page-table">
                    <thead>
                        <tr>
                            <th>Book ID</th>
                            <th>Title</th>
                            <th>Author</th>
                            <th>Publisher</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {books.map((book) => (
                            <tr key={book.bookId}>
                                <td>{book.bookId}</td>
                                <td>{book.title}</td>
                                <td>{book.author}</td>
                                <td>{book.publisherId}</td>
                                <td>
                                    <Link to={`/admin/book-form/${book.bookId}`} className="book-list-page-action-button detail">Detail</Link>
                                    <button onClick={() => handleDelete(book.bookId)} className="book-list-page-action-button delete">Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </>
    );
};

export default BookListPage;