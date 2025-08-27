import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/GenreList.css';

const GenreListPage = () => {
    const [genres, setGenres] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchGenres();
        }
    }, [navigate]);

    const fetchGenres = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/genre');
            const data = await response.json();
            setGenres(data);
        } catch (error) {
            console.error("Error fetching genres:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this genre? This action cannot be undone.")) {
            try {
                await fetch(`http://localhost:5231/api/genre/${id}`, {
                    method: 'DELETE',
                });
                fetchGenres();
            } catch (error) {
                console.error("Error deleting genre:", error);
            }
        }
    };

    return (
        <>
            <div className="genre-list-page-container">
                <h2>Genre List</h2>
                <Link to="/admin/genre-form" className="genre-list-page-add-button">Add Genre</Link>
                <table className="genre-list-page-table">
                    <thead>
                        <tr>
                            <th>Genre ID</th>
                            <th>Genre Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {genres.map((genre) => (
                            <tr key={genre.genreId}>
                                <td>{genre.genreId}</td>
                                <td>{genre.genreName}</td>
                                <td>
                                    <Link to={`/admin/genre-form/${genre.genreId}`} className="genre-list-page-action-button detail">Detail</Link>
                                    <button onClick={() => handleDelete(genre.genreId)} className="genre-list-page-action-button delete">Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </>
    );
};

export default GenreListPage;