import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Home from './components/Home';
import ProductDetail from './components/ProductDetail';
import ProductsList from './components/ProductsList';
import Header from './components/Header';
import Login from './components/Login';
import Register from './components/Register';
import Cart from './components/Cart';
import AddProduct from './components/AddProduct';
import UpdateProduct from './components/UpdateProduct'; // Import UpdateProduct
import { CartProvider } from './context/CartContext'; // Import CartProvider

const App = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('token')); // Update initial state

    const handleLogin = () => {
        setIsAuthenticated(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        setIsAuthenticated(false);
    };

    return (
        <CartProvider> {/* Wrap the application with CartProvider */}
            <Router>
                <Header isAuthenticated={isAuthenticated} onLogout={handleLogout} />
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/products" element={<ProductsList />} />
                    <Route path="/product/:id" element={<ProductDetail />} />
                    <Route path="/login" element={<Login onLogin={handleLogin} />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/cart" element={isAuthenticated ? <Cart /> : <Navigate to="/login" state={{ from: '/cart' }} />} />
                    <Route path="/add-product" element={isAuthenticated ? <AddProduct /> : <Navigate to="/login" state={{ from: '/add-product' }} />} />
                    <Route path="/update-product/:id" element={isAuthenticated ? <UpdateProduct /> : <Navigate to="/login" />} />
                    <Route path="*" element={<Navigate to="/" />} />
                </Routes>
            </Router>
        </CartProvider>
    );
};

export default App;
