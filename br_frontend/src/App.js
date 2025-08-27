import logo from './logo.svg';
import './App.css';
import Header from './components/Header';
import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import HomePage from './pages/HomePage';
import BookDetails from './pages/Book/BookDetail';
import Cart from './pages/Cart/Cart';
import Invoices from './pages/Account/Invoice';
import Account from './pages/Account/Account';
import SearchResults from './pages/SearchResult';
import InvoiceDetail from './pages/Account/InvoiceDetail';
import UserReviews from './pages/Account/UserReview';
import ReviewBook from './pages/Book/ReviewBook';
import ChangePassword from './pages/Account/ChangePassword';
import DashboardPage from './pages/Admin/Dashboard';
import GenreListPage from './pages/Admin/Genre/GenreListPage';
import GenreFormPage from './pages/Admin/Genre/GenreFormPage';
import PublisherListPage from './pages/Admin/Publisher/PublisherListPage';
import PublisherFormPage from './pages/Admin/Publisher/PublisherFormPage';
import BookListPage from './pages/Admin/Book/BookListPage';
import BookFormPage from './pages/Admin/Book/BookFormPage';
import AdminListPage from './pages/Admin/AdminAccount/AdminListPage';
import AdminFormPage from './pages/Admin/AdminAccount/AdminFormPage';
import StaffListPage from './pages/Admin/OtherAccount/StaffListPage';
import LesseListPage from './pages/Admin/OtherAccount/LesseListPage';
import CreateUserPage from './pages/Admin/OtherAccount/CreateUserPage';
import InvoiceListPage from './pages/Admin/Invoice/InvoiceListPage';
import InvoiceDetailPage from './pages/Admin/Invoice/InvoiceDetailPage';
import ReviewsListPage from './pages/Admin/Review/ReviewListPage';
function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(
    localStorage.getItem('token') !== null
  );

  const handleLoginSuccess = () => {
    setIsAuthenticated(true);
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    setIsAuthenticated(false);
  };

  return (
    <Router>
      <Routes>
        <Route
          path="/login"
          element={
            isAuthenticated ? (
              <Navigate to="/" />
            ) : (
              <LoginPage onLoginSuccess={handleLoginSuccess} />
            )
          }
        />
        <Route
          path="/register"
          element={isAuthenticated ? <Navigate to="/" /> : <RegisterPage />}
        />
        <Route
          path="/"
          element={
            isAuthenticated ? (
              <HomePage onLogout={handleLogout} />
            ) : (
              <Navigate to="/login" />
            )
          }
        />
        <Route
          path="/book/:id"
          element={<BookDetails />}
        />
        <Route path="/search" element={<SearchResults />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/invoices" element={isAuthenticated ? <Invoices /> : <Navigate to="/login" />} />
        <Route path="/invoice/:id" element={<InvoiceDetail />} />
        <Route path="/account" element={isAuthenticated ? <Account /> : <Navigate to="/login" />} />
        <Route path="/review" element={<UserReviews />} />
        <Route path="/review-book" element={<ReviewBook />} />
        <Route path="/change-password" element={<ChangePassword />} />
        <Route path="/admin/dashboard" element={<DashboardPage />} />
        <Route path="/admin/genre-list" element={<GenreListPage />} />
        <Route path="/admin/genre-form/:id" element={<GenreFormPage />} />
        <Route path="/admin/genre-form" element={<GenreFormPage />} />
        <Route path="/admin/publisher-list" element={<PublisherListPage />} />
        <Route path="/admin/publisher-form" element={<PublisherFormPage />} />
        <Route path="/admin/publisher-form/:id" element={<PublisherFormPage />} />
        <Route path="/admin/book-list" element={<BookListPage />} />
        <Route path="/admin/book-form" element={<BookFormPage />} />
        <Route path="/admin/book-form/:id" element={<BookFormPage />} />
        <Route path="/admin/admin-list" element={<AdminListPage />} />
        <Route path="/admin/admin-form/:id" element={<AdminFormPage />} />
        <Route path="/admin/admin-form" element={<AdminFormPage />} />
        <Route path="/admin/staff-list" element={<StaffListPage />} />
        <Route path="/admin/lesse-list" element={<LesseListPage />} />
        <Route path="/admin/create-user" element={<CreateUserPage />} />
        <Route path="/admin/invoice-list" element={<InvoiceListPage />} />
        <Route path="/admin/invoice-form/:id" element={<InvoiceDetailPage />} />
        <Route path="/admin/review-list" element={<ReviewsListPage />} />
      </Routes>
    </Router>
  );
}

export default App;