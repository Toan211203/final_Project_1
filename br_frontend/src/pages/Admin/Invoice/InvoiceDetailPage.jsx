import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/InvoiceDetail.css';

const InvoiceDetailPage = () => {
    const { invoiceId } = useParams();
    const [invoice, setInvoice] = useState(null);
    const [paymentStatus, setPaymentStatus] = useState(0);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            console.log("Fetching invoice details for ID:", invoiceId);
            fetchInvoiceDetails();
        }
    }, [navigate, invoiceId]);

    const fetchInvoiceDetails = async () => {
        if (!invoiceId) {
            console.error("Invoice ID is undefined");
            return;
        }

        try {
            const response = await fetch(`http://localhost:5231/api/invoice/${invoiceId}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            setInvoice(data);
            setPaymentStatus(data?.paymentStatus || 0);
        } catch (error) {
            console.error("Error fetching invoice details:", error);
        }
    };

    const handleStatusChange = async (event) => {
        const newStatus = event.target.value;
        setPaymentStatus(newStatus);

        try {
            await fetch(`http://localhost:5231/api/invoice/${invoiceId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ paymentStatus: newStatus }),
            });
            alert("Payment status updated successfully!");
        } catch (error) {
            console.error("Error updating payment status:", error);
        }
    };

    if (!invoice) return <div>Loading...</div>;

    return (
        <div className="invoice-detail-page-container">
            <h2>Invoice Details</h2>
            <p><strong>Invoice ID:</strong> {invoice.invoiceId}</p>
            <p><strong>Rental ID:</strong> {invoice.rentalId}</p>
            <p><strong>User ID:</strong> {invoice.userId}</p>
            <p><strong>Invoice Date:</strong> {new Date(invoice.invoiceDate).toLocaleDateString()}</p>
            <p><strong>Total Amount:</strong> ${invoice.totalAmount ? invoice.totalAmount.toFixed(2) : 'N/A'}</p>

            <label htmlFor="paymentStatus">Payment Status:</label>
            <select id="paymentStatus" value={paymentStatus} onChange={handleStatusChange}>
                <option value="0">Not Checked Out</option>
                <option value="1">Has Checked Out</option>
            </select>

            <h3>Rental Information</h3>
            <p><strong>Rental ID:</strong> {invoice.rental?.rentalId}</p>
            <p><strong>User ID:</strong> {invoice.rental?.userId}</p>
            <p><strong>Rental Date:</strong> {new Date(invoice.rental?.rentalDate).toLocaleDateString()}</p>
            <p><strong>Due Date:</strong> {new Date(invoice.rental?.dueDate).toLocaleDateString()}</p>
            <p><strong>Return Date:</strong> {invoice.rental?.returnDate ? new Date(invoice.rental.returnDate).toLocaleDateString() : 'Not Returned'}</p>
            <p><strong>Status:</strong> {invoice.rental?.status}</p>
            <p><strong>Total Cost:</strong> ${invoice.rental?.totalCost ? invoice.rental.totalCost.toFixed(2) : 'N/A'}</p>

            <h3>Rental Details</h3>
            <table className="rental-details-table">
                <thead>
                    <tr>
                        <th>Book Title</th>
                        <th>Quantity</th>
                    </tr>
                </thead>
                <tbody>
                    {invoice.rental?.rentalDetails.map(detail => (
                        <tr key={detail.rentalDetailId}>
                            <td>{detail.bookTitle}</td>
                            <td>{detail.quantity}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default InvoiceDetailPage;