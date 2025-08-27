import React, { useEffect, useState } from 'react';
import { getAuthHeaders, getUserRoleFromToken } from '../../../services/userAuth';
import { Link, useNavigate } from 'react-router-dom';

const API_BASE_URL = 'http://localhost:5231';

const Invoices = () => {
  const [invoices, setInvoices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const userRole = getUserRoleFromToken();
    const navigate = useNavigate();
    if (userRole !== '1') {
      alert("You do not have permission to view this page.");
        navigate('/');
      return;
    }
    else{
      fetchInvoices();
    }
  }, [useNavigate]);

  const fetchInvoices = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/api/invoice`, {
          headers: getAuthHeaders(),
        });

        if (!response.ok) {
          throw new Error(`Error fetching invoices: ${response.status}`);
        }

        const data = await response.json();
        setInvoices(data);
        setLoading(false);
      } catch (err) {
        setError(err.message);
        setLoading(false);
      }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this invoice? This action cannot be undone.")) {
            try {
                await fetch(`http://localhost:5231/api/invoice/${id}`, {
                    method: 'DELETE',
                });
                fetchInvoices();
            } catch (error) {
                console.error("Error deleting book:", error);
            }
        }
    };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <div style={{ padding: '1rem' }}>
        <h2>Invoices</h2>
        {invoices.length === 0 ? (
          <p>No invoices available.</p>
        ) : (
          <table style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr>
                <th style={{ border: '1px solid #ccc', padding: '0.5rem' }}>Invoice ID</th>
                <th style={{ border: '1px solid #ccc', padding: '0.5rem' }}>Invoice Date</th>
                <th style={{ border: '1px solid #ccc', padding: '0.5rem' }}>Total Amount</th>
                <th style={{ border: '1px solid #ccc', padding: '0.5rem' }}>Actions</th>
              </tr>
            </thead>
            <tbody>
              {invoices.map(invoice => (
                <tr key={invoice.invoiceId}>
                  <td style={{ border: '1px solid #ccc', padding: '0.5rem' }}>{invoice.invoiceId}</td>
                  <td style={{ border: '1px solid #ccc', padding: '0.5rem' }}>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
                  <td style={{ border: '1px solid #ccc', padding: '0.5rem' }}>{invoice.totalAmount.toLocaleString()}đ</td>
                  <td style={{ border: '1px solid #ccc', padding: '0.5rem' }}>
                    <Link to={`/admin/invoice-form/${invoice.invoiceId}`}>Detail</Link>
                    <button onClick={() => handleDelete(invoice.invoiceId)}>Delete</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </>
  );
};

export default InvoiceListPage;