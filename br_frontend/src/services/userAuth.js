import { jwtDecode } from 'jwt-decode';

export const getUserIdFromToken = () => {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
        const decoded = jwtDecode(token);
        return decoded.UserId || decoded.userId || decoded.id;
    } catch (err) {
        console.error('Token decode error:', err);
        return null;
    }
};

export const getUserRoleFromToken = () => {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
        const decoded = jwtDecode(token);
        return decoded.role || decoded.Role;
    } catch (err) {
        console.error('Token decode error:', err);
        return null;
    }
};

export const getAuthHeaders = () => {
    const token = localStorage.getItem('token');
    return {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
    };
};