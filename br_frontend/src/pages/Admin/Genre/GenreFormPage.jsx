import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';
import '../CSS/GenreForm.css';

const API_URL = 'http://localhost:5231/api/genre';

const GenreFormPage = () => {
    const [genre, setGenre] = useState({ genreName: '' });
    const [isEdit, setIsEdit] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            setIsEdit(true);
            fetchGenre(id);
        }
    }, [id]);

    const fetchGenre = async (id) => {
        try {
            const response = await fetch(`${API_URL}/${id}`, {
                headers: getAuthHeaders(),
            });
            const data = await response.json();
            setGenre(data);
        } catch (error) {
            console.error("Error fetching genre:", error);
            setError("Failed to fetch genre.");
        }
    };

    const handleChange = (e) => {
        setGenre({ ...genre, [e.target.name]: e.target.value });
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
                    body: JSON.stringify(genre),
                });
            } else {
                await fetch(API_URL, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(genre),
                });
            }
            navigate('/admin/genre-list');
        } catch (error) {
            console.error("Error saving genre:", error);
            setError("Failed to save genre.");
        }
    };

    return (
        <div className="genre-form-page-container">
            <h2>{isEdit ? 'Update Genre' : 'Add Genre'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="input-group">
                    <label>Genre Name:</label>
                    <input
                        type="text"
                        name="genreName"
                        value={genre.genreName}
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

export default GenreFormPage;