import React, { useEffect, useState } from 'react';
import { getUserRoleFromToken } from '../../../services/userAuth';
import { useNavigate } from 'react-router-dom';
import '../CSS/ReviewList.css';

const ReviewsListPage = () => {
    const [reviews, setReviews] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchReviews();
        }
    }, [navigate]);

    const fetchReviews = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/review');
            const data = await response.json();
            setReviews(data);
        } catch (error) {
            console.error("Error fetching reviews:", error);
        }
    };

    return (
        <div className="reviews-list-page-container">
            <h2>Reviews List</h2>
            <table className="reviews-list-page-table">
                <thead>
                    <tr>
                        <th>Book Title</th>
                        <th>Username</th>
                        <th>Rating</th>
                        <th>Comment</th>
                    </tr>
                </thead>
                <tbody>
                    {reviews.map((review) => (
                        <tr key={review.reviewId}>
                            <td>{review.bookTitle}</td>
                            <td>{review.username}</td>
                            <td>{review.rating}</td>
                            <td>{review.comment}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default ReviewsListPage;