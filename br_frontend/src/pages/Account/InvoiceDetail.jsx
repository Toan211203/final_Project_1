import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../services/userAuth';
import Header from '../../components/Header';
import Sidebar from '../../components/Sidebar';
import '../../components/CSS/InvoiceDetail.css';

const API_BASE_URL = 'http://localhost:5231';

const InvoiceDetail = () => {
    const { id } = useParams();
    const [invoice, setInvoice] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchInvoiceDetail = async () => {
            setLoading(true);
            try {
                const response = await axios.get(`${API_BASE_URL}/api/invoice/${id}`, {
                    headers: getAuthHeaders(),
                });
                setInvoice(response.data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchInvoiceDetail();
    }, [id]);

    if (loading) return <div className="loading">Loading...</div>;
    if (error) return <div className="error">{error}</div>;

    return (
        <>
            <Header />
            <Sidebar />
            <div className="invoice-detail-container">
                <h2>Invoice Detail</h2>
                <div className="invoice-summary">
                    <p><strong>Invoice ID:</strong> {invoice.invoiceId}</p>
                    <p><strong>Invoice Date:</strong> {new Date(invoice.invoiceDate).toLocaleDateString()}</p>
                    <p><strong>Total Amount:</strong> {invoice.totalAmount.toFixed(2)} VNĐ</p>
                    <p><strong>Status:</strong> {invoice.paymentStatus === 1 ? 'Has checkout' : 'Not checkout'}</p>
                </div>

                <h3>Rental Details</h3>
                <ul className="rental-details">
                    {invoice.rental?.rentalDetails.map((detail) => (
                        <li key={detail.rentalDetailId}>
                            <span className="book-title">{detail.bookTitle}</span> - <span className="quantity">Quantity: {detail.quantity}</span>
                        </li>
                    )) || <li>No rental details available</li>}
                </ul>
            </div>
        </>
    );
};

export default InvoiceDetail;