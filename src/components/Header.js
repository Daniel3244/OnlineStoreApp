import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Header.css'; 

const Header = ({ isAuthenticated, onLogout }) => {
    const navigate = useNavigate();
    const username = localStorage.getItem('username'); 
  

    const handleLogout = () => {
        onLogout();
        navigate('/');
    };

    return (
        <header className="header">
            <nav className="navbar">
                <div className="navbar-brand-container">
                    <Link to="/" className="navbar-brand">Online Store</Link>
                    {isAuthenticated && <span className="navbar-greeting">Hello, {username}. </span>}
                </div>
                <div className="navbar-items">
                    <Link to="/products" className="navbar-item">Products</Link>
                    <Link to="/cart" className="navbar-item">Cart</Link>
                    {isAuthenticated ? (
                        <button onClick={handleLogout} className="btn btn-logout">Logout</button>
                    ) : (
                        <>
                            <Link to="/login" className="navbar-item">Login</Link>
                            <Link to="/register" className="navbar-item">Register</Link>
                        </>
                    )}
                </div>
            </nav>
        </header>
    );
};

export default Header;
