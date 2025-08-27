import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/PublisherList.css';

const PublisherListPage = () => {
    const [publishers, setPublishers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchPublishers();
        }
    }, [navigate]);

    const fetchPublishers = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/publisher');
            const data = await response.json();
            setPublishers(data);
        } catch (error) {
            console.error("Error fetching publishers:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this publisher? This action cannot be undone.")) {
            try {
                await fetch(`http://localhost:5231/api/publisher/${id}`, {
                    method: 'DELETE',
                });
                fetchPublishers();
            } catch (error) {
                console.error("Error deleting publisher:", error);
            }
        }
    };

    return (
        <>
            <div className="publisher-list-page-container">
                <h2>Publisher List</h2>
                <Link to="/admin/publisher-form" className="publisher-list-page-add-button">Add Publisher</Link>
                <table className="publisher-list-page-table">
                    <thead>
                        <tr>
                            <th>Publisher ID</th>
                            <th>Publisher Name</th>
                            <th>Contact Person</th>
                            <th>Phone Number</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {publishers.map((publisher) => (
                            <tr key={publisher.publisherId}>
                                <td>{publisher.publisherId}</td>
                                <td>{publisher.publisherName}</td>
                                <td>{publisher.contactPerson}</td>
                                <td>{publisher.phoneNumber}</td>
                                <td>
                                    <Link to={`/admin/publisher-form/${publisher.publisherId}`} className="publisher-list-page-action-button detail">Detail</Link>
                                    <button onClick={() => handleDelete(publisher.publisherId)} className="publisher-list-page-action-button delete">Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </>
    );
};

export default PublisherListPage;