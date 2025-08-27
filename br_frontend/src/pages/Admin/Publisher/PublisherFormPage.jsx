import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';
//import '../CSS/PublisherForm.css';

const API_URL = 'http://localhost:5231/api/publisher';

const PublisherFormPage = () => {
    const [publisher, setPublisher] = useState({
        publisherName: '',
        contactPerson: '',
        phoneNumber: '',
        email: ''
    });
    const [isEdit, setIsEdit] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            setIsEdit(true);
            fetchPublisher(id);
        }
    }, [id]);

    const fetchPublisher = async (id) => {
        try {
            const response = await fetch(`${API_URL}/${id}`, {
                headers: getAuthHeaders(),
            });
            const data = await response.json();
            setPublisher(data);
        } catch (error) {
            console.error("Error fetching publisher:", error);
            setError("Failed to fetch publisher.");
        }
    };

    const handleChange = (e) => {
        setPublisher({ ...publisher, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const upsertPublisherDTO = {
            publisherName: publisher.publisherName,
            contactPerson: publisher.contactPerson,
            phoneNumber: publisher.phoneNumber,
            email: publisher.email
        };

        try {
            if (isEdit) {
                await fetch(`${API_URL}/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(upsertPublisherDTO),
                });
            } else {
                await fetch(API_URL, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        ...getAuthHeaders(),
                    },
                    body: JSON.stringify(upsertPublisherDTO),
                });
            }
            navigate('/admin/publisher-list');
        } catch (error) {
            console.error("Error saving publisher:", error);
            setError("Failed to save publisher.");
        }
    };

    return (
        <div className="publisher-form-page-container">
            <h2>{isEdit ? 'Update Publisher' : 'Add Publisher'}</h2>
            {error && <div className="error-message">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="input-group">
                    <label>Publisher Name:</label>
                    <input
                        type="text"
                        name="publisherName"
                        value={publisher.publisherName}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Contact Person:</label>
                    <input
                        type="text"
                        name="contactPerson"
                        value={publisher.contactPerson}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Phone Number:</label>
                    <input
                        type="text"
                        name="phoneNumber"
                        value={publisher.phoneNumber}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="input-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={publisher.email}
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

export default PublisherFormPage;