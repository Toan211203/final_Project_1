import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { getUserIdFromToken, getAuthHeaders } from '../../services/userAuth';
import Header from '../../components/Header';
import Sidebar from '../../components/Sidebar';
import '../../components/CSS/UserReview.css';

const API_BASE_URL = 'http://localhost:5231';

const UserReviews = () => {
    const [reviews, setReviews] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchReviews = async () => {
            setLoading(true);
            const userId = getUserIdFromToken();
            if (!userId) {
                setError("Can't identify user");
                setLoading(false);
                return;
            }

            try {
                const response = await axios.get(`${API_BASE_URL}/api/review/user/${userId}`, {
                    headers: getAuthHeaders(),
                });
                setReviews(response.data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchReviews();
    }, []);

    const renderStars = (rating) => {
        const stars = [];
        for (let i = 0; i < 5; i++) {
            stars.push(
                <span key={i} className={i < rating ? 'star filled' : 'star'}>★</span>
            );
        }
        return stars;
    };

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <>
            <Header />
            <Sidebar />
            <div className="user-reviews-container">
                <h2>User Reviews</h2>
                <ul className="reviews-list">
                    {reviews.length > 0 ? (
                        reviews.map((review) => (
                            <li key={review.reviewId} className="review-item">
                                <p className="review-book-title">{review.bookTitle}</p>
                                <div className="review-rating">
                                    {renderStars(review.rating)}
                                </div>
                                <p className="review-content">{review.comment}</p>
                            </li>
                        ))
                    ) : (
                        <li>No reviews available</li>
                    )}
                </ul>
            </div>
        </>
    );
};

export default UserReviews;