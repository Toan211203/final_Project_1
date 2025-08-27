import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { getUserRoleFromToken } from '../../../services/userAuth';
import '../CSS/InvoiceList.css';

const InvoiceListPage = () => {
    const [invoices, setInvoices] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const role = getUserRoleFromToken();

        if (role !== '1') {
            navigate('/');
        } else {
            fetchInvoices();
        }
    }, [navigate]);

    const fetchInvoices = async () => {
        try {
            const response = await fetch('http://localhost:5231/api/invoice');
            const data = await response.json();
            setInvoices(data);
        } catch (error) {
            console.error("Error fetching invoices:", error);
        }
    };

    return (
        <div className="invoice-list-page-container">
            <h2>Invoice List</h2>
            <table className="invoice-list-page-table">
                <thead>
                    <tr>
                        <th>Invoice ID</th>
                        <th>Rental ID</th>
                        <th>Invoice Date</th>
                        <th>Total Amount</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {invoices.map((invoice) => (
                        <tr key={invoice.invoiceId}>
                            <td>{invoice.invoiceId}</td>
                            <td>{invoice.rentalId}</td>
                            <td>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
                            <td>{invoice.totalAmount ? invoice.totalAmount.toFixed(2) : 'N/A'} VND</td>
                            <td>
                                <Link to={`/admin/invoice-form/${invoice.invoiceId}`} className="invoice-list-page-action-button">Update</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default InvoiceListPage;