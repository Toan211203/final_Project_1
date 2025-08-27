import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { getUserIdFromToken } from '../../services/userAuth';
import Header from '../../components/Header';
import '../../components/CSS/ReviewBook.css';

const API_BASE_URL = 'http://localhost:5231';

const ReviewBook = () => {
    const location = useLocation();
    const { bookId, bookTitle } = location.state;
    const [rating, setRating] = useState(1);
    const [reviewComment, setReviewComment] = useState('');
    const navigate = useNavigate();

    const handleSubmitReview = async () => {
        const userId = getUserIdFromToken();
        if (!userId) {
            alert('Please log in before submitting a review.');
            return;
        }

        const payload = {
            userId,
            bookId,
            rating,
            comment: reviewComment,
        };

        try {
            const response = await fetch(`${API_BASE_URL}/api/review`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${localStorage.getItem('token')}`
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                alert('Review submitted successfully!');
                navigate(`/book/${bookId}`); // Redirect to book details page
            } else {
                const err = await response.json();
                alert(`Error submitting review: ${err.message || response.statusText}`);
            }
        } catch (err) {
            console.error('API call error:', err);
            alert('An error occurred, please try again later.');
        }
    };

    return (
        <div className="review-book-container">
            <Header />
            <div className="new-container mt-4">
                <h2>Rate {bookTitle}</h2>
                <div className="mb-3">
                    <label htmlFor="rating" className="form-label">Rating:</label>
                    <select
                        id="rating"
                        className="form-select"
                        value={rating}
                        onChange={(e) => setRating(parseInt(e.target.value))}
                    >
                        {[1, 2, 3, 4, 5].map((star) => (
                            <option key={star} value={star}>{star}</option>
                        ))}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="reviewComment" className="form-label">Comment:</label>
                    <textarea
                        id="reviewComment"
                        className="form-control"
                        value={reviewComment}
                        onChange={(e) => setReviewComment(e.target.value)}
                        rows="4"
                    />
                </div>
                <button className="btn btn-primary" onClick={handleSubmitReview}>
                    Submit Review
                </button>
            </div>
        </div>
    );
};

export default ReviewBook;