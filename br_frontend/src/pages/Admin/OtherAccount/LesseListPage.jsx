import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/LesseList.css';

const LesseListPage = () => {
    const [lesse, setLesse] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchLesse();
        }
    }, [navigate]);

    const fetchLesse = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/lesse/all');
            const data = await response.json();
            setLesse(data);
        } catch (error) {
            console.error("Error fetching lesse:", error);
        }
    };

    return (
        <div className="lesse-list-page-container">
            <h2>Lesse Accounts</h2>
            <Link to="/admin/create-user" className="lesse-list-page-add-button">Add Lesse</Link>
            <table className="lesse-list-page-table">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Full Name</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                    </tr>
                </thead>
                <tbody>
                    {lesse.map((member) => (
                        <tr key={member.userId}>
                            <td>{member.username}</td>
                            <td>{member.fullName}</td>
                            <td>{member.email}</td>
                            <td>{member.phoneNumber}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default LesseListPage;