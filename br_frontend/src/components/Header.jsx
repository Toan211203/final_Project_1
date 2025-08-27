import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../services/userAuth';
import './CSS/Header.css';

const API_BASE_URL = 'http://localhost:5231';

const Header = () => {
    const navigate = useNavigate();
    const isAuthenticated = localStorage.getItem('token') !== null;
    const username = localStorage.getItem('username');
    const [genres, setGenres] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        const fetchGenres = async () => {
            try {
                const response = await fetch(`${API_BASE_URL}/api/genre`);
                if (!response.ok) throw new Error('Failed to fetch genres');
                const data = await response.json();
                setGenres(data);
            } catch (error) {
                console.error('Error fetching genres:', error);
            }
        };

        fetchGenres();
    }, []);

    const handleLoginClick = () => {
        if (!isAuthenticated) navigate('/login');
    };

    const handleCartClick = () => {
        if (!isAuthenticated) {
            navigate('/login');
        } else {
            navigate('/cart');
        }
    };

    const handleGenreSelect = (genreId) => {
        console.log(`Selected Genre ID: ${genreId}`);
        navigate(`/search?genreId=${genreId}`);
    };

    const handleSearch = () => {
        navigate(`/search?query=${searchTerm}`);
    };

    const handleLogoClick = () => {
        navigate('/');
    };

    const userRole = getUserRoleFromToken();

    return (
        <header className="header-custom">
            <div className="header-top-row">
                <div className="header-logo" onClick={handleLogoClick}>
                    <div className="header-line1">BOOK RENTAL</div>
                    <div className="header-line2">WEBSITE</div>
                </div>
                <nav className="header-nav-links">
                    {userRole !== '1' && (
                        <button onClick={handleCartClick}>Cart</button>
                    )}
                    {isAuthenticated ? (
                        <>
                            <span className="header-username">Hello, {username}</span>
                            {userRole === '1' ? (
                                <button onClick={() => navigate('/admin/dashboard')}>Dashboard</button>
                            ) : (
                                <button onClick={() => navigate('/account')}>Info</button>
                            )}
                        </>
                    ) : (
                        <button onClick={handleLoginClick}>Login</button>
                    )}
                </nav>
            </div>
            <div className="header-bottom-row">
                <select onChange={(e) => handleGenreSelect(e.target.value)}>
                    <option value="">All Genres</option>
                    {genres.map((genre) => (
                        <option key={genre.genreId} value={genre.genreId}>
                            {genre.genreName}
                        </option>
                    ))}
                </select>
                <div className="header-search-bar">
                    <input
                        type="text"
                        placeholder="Search for book, author..."
                        className="header-searchbar"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    <button onClick={handleSearch}>Search</button>
                </div>
            </div>
        </header>
    );
};

export default Header;