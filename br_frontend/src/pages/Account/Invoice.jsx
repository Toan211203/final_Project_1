import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import '../../components/CSS/Invoice.css';
import { getUserIdFromToken, getAuthHeaders } from '../../services/userAuth';
import Header from '../../components/Header';
import Sidebar from '../../components/Sidebar';

const API_BASE_URL = 'http://localhost:5231';

const Invoices = () => {
    const [invoices, setInvoices] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchInvoices = async () => {
            setLoading(true);
            const userId = getUserIdFromToken();
            if (!userId) {
                setError("Can't identify user");
                setLoading(false);
                return;
            }

            try {
                const response = await axios.get(`http://localhost:5231/api/invoice/user/${userId}`, {
                    headers: getAuthHeaders(),
                });
                setInvoices(response.data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchInvoices();
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <>
            <Header />
            <Sidebar />
            <div className="invoices-container">
                <div className="header">
                    <h2>All invoices</h2>
                </div>
                <table className="invoices-table">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>Invoice ID</th>
                            <th>Invoice Date</th>
                            <th>Total</th>
                            <th>Status</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {invoices.length > 0 ? (
                            invoices.map((invoice, index) => (
                                <tr key={invoice.invoiceId}>
                                    <td>{index + 1}</td>
                                    <td>{invoice.invoiceId}</td>
                                    <td>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
                                    <td>{invoice.totalAmount.toFixed(2)} VNĐ</td>
                                    <td>{invoice.paymentStatus === 1 ? 'Has checkout' : 'Not checkout'}</td>
                                    <td>
                                        <Link to={`/invoice/${invoice.invoiceId}`}>
                                            <button>Detail</button>
                                        </Link>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan="6">Empty Invoices</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </>
    );
};

export default Invoices;