import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getAuthHeaders } from '../../../services/userAuth';

const InvoiceDetails = () => {
  const { invoiceId } = useParams();
  const [invoice, setInvoice] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchInvoiceDetails = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/api/invoices/${invoiceId}`, {
          headers: getAuthHeaders(),
        });

        if (!response.ok) {
          throw new Error(`Error fetching invoice details: ${response.status}`);
        }

        const data = await response.json();
        setInvoice(data);
        setLoading(false);
      } catch (err) {
        setError(err.message);
        setLoading(false);
      }
    };

    fetchInvoiceDetails();
  }, [invoiceId]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error}</div>;

  return (
    <>
      <div style={{ padding: '1rem' }}>
        <h2>Invoice Details</h2>
        {invoice ? (
          <div>
            <p><strong>Invoice ID:</strong> {invoice.invoiceId}</p>
            <p><strong>User ID:</strong> {invoice.userId}</p>
            <p><strong>Invoice Date:</strong> {new Date(invoice.invoiceDate).toLocaleDateString()}</p>
            <p><strong>Total Amount:</strong> {invoice.totalAmount.toFixed(2)}đ</p>
            <p><strong>Payment status:</strong> {invoice.paymentStatus === 1 ? 'Has checkout' : 'Not checkout'}</p>
          </div>
        ) : (
          <p>No details available for this invoice.</p>
        )}
      </div>
    </>
  );
};

export default InvoiceDetails;