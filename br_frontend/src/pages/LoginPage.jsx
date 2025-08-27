import React from 'react';
import LoginForm from '../components/LoginForm';
import { Link } from 'react-router-dom';
import Header from '../components/Header';
import '../components/CSS/log.css';

const LoginPage = ({ onLoginSuccess }) => {
  return (
    <>
      <Header />
      <div className="container">
        <div className="card">
          <h2 className="card-title">login</h2>
          <LoginForm onLoginSuccess={onLoginSuccess} />
          <div className="text-center">
            <p>
              Don't have account?{' '}
              <Link to="/register" className="text-link">
                Register here
              </Link>
            </p>
            <p>
              <Link to="/forgot-password" className="text-link">
                Forgot password?
              </Link>
            </p>
          </div>
        </div>
      </div>
    </>
  );
};

export default LoginPage;