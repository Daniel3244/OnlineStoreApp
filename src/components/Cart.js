import React, { useState, useEffect } from 'react';
import { useCart } from '../context/CartContext';
import { useNavigate } from 'react-router-dom';
import { Modal, Button } from 'react-bootstrap';
import api from '../services/api';
import { jwtDecode } from 'jwt-decode'; 
import './Cart.css'; 

const Cart = () => {
    const { cart, dispatch } = useCart();
    const [showThankYou, setShowThankYou] = useState(false);
    const [error, setError] = useState(null);
    const [stockErrors, setStockErrors] = useState([]); 
    const [showStockModal, setShowStockModal] = useState(false);
    const navigate = useNavigate();
    const token = localStorage.getItem('token');
    const isAuthenticated = !!token;
    const userId = token ? jwtDecode(token).id : null; 

    useEffect(() => {
        if (!isAuthenticated) {
            navigate('/login', { state: { from: { pathname: '/cart' } } }); 
        }
    }, [isAuthenticated, navigate]);

    const handleRemove = (id) => {
        dispatch({ type: 'REMOVE_FROM_CART', id });
    };

    const handleQuantityChange = (id, quantity) => {
        if (quantity >= 1) {
            dispatch({ type: 'UPDATE_QUANTITY', id, quantity });
        }
    };

    const handleCheckout = async () => {
        try {
            const errors = [];
            for (let product of cart) {
                const response = await api.fetchProductById(product.id);
                const availableStock = response.data.stock;

                if (product.quantity > availableStock) {
                    errors.push({
                        ...product,
                        actualStock: availableStock
                    });
                }
            }

            if (errors.length > 0) {
                setStockErrors(errors);
                setShowStockModal(true);
                return;
            }

            await placeOrder(cart);

            setShowThankYou(true);
            dispatch({ type: 'CLEAR_CART' });
        } catch (err) {
            setError('Failed to create order. Please try again.');
            console.error(err.response ? err.response.data : err.message); 
        }
    };

    const placeOrder = async (cartItems) => {
        const orderItems = cartItems.map(product => ({
            productId: product.id,
            quantity: product.quantity,
            price: product.price
        }));
        const totalPrice = calculateTotalPrice(cartItems);

        const order = {
            userId, 
            orderItems,
            totalPrice
        };

        await api.createOrder(order);
    };

    const handleConfirmStock = async () => {
        const updatedCart = cart.map(product => {
            const error = stockErrors.find(e => e.id === product.id);
            if (error) {
                return { ...product, quantity: error.actualStock };
            }
            return product;
        });

        dispatch({ type: 'UPDATE_CART', updatedCart });
        setShowStockModal(false);

        await placeOrder(updatedCart);

        setShowThankYou(true);
        dispatch({ type: 'CLEAR_CART' });
    };

    const calculateTotalPrice = (cartItems) => {
        return cartItems.reduce((total, product) => total + product.price * product.quantity, 0);
    };

    if (!isAuthenticated) {
        return null;
    }

    return (
        <div className="container mt-5">
            <h1>Cart</h1>
            {error && <div className="alert alert-danger">{error}</div>}
            {showThankYou ? (
                <div className="alert alert-success">Thanks for shopping!</div>
            ) : (
                <>
                    <ul className="list-group mb-4">
                        {cart.map(product => (
                            <li key={product.id} className="list-group-item d-flex justify-content-between align-items-center">
                                <div>
                                    {product.name} - ${product.price.toFixed(2)}
                                </div>
                                <div className="d-flex align-items-center">
                                    <button
                                        onClick={() => handleQuantityChange(product.id, product.quantity - 1)}
                                        className="btn btn-outline-secondary btn-sm"
                                    >-</button>
                                    <span className="mx-2 quantity-number">{product.quantity}</span>
                                    <button
                                        onClick={() => handleQuantityChange(product.id, product.quantity + 1)}
                                        className="btn btn-outline-secondary btn-sm"
                                    >+</button>
                                    <button onClick={() => handleRemove(product.id)} className="btn btn-danger btn-sm ml-3">Remove</button>
                                </div>
                            </li>
                        ))}
                    </ul>
                    <h4>Total Price: ${calculateTotalPrice(cart).toFixed(2)}</h4>
                    <button 
                        onClick={handleCheckout} 
                        className="btn btn-primary" 
                        disabled={cart.length === 0}
                    >
                        Checkout
                    </button>
                </>
            )}

            <Modal show={showStockModal} onHide={() => setShowStockModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Stock Issues</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {stockErrors.map((error, index) => (
                        <div key={index}>
                            <p><strong>{error.name}</strong> - Requested: {error.quantity}, Available: {error.actualStock}</p>
                        </div>
                    ))}
                    <p>Would you like to adjust the quantities and proceed?</p>
                    <p>New Total Price: ${calculateTotalPrice(stockErrors.map(e => ({ ...e, quantity: e.actualStock }))).toFixed(2)}</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowStockModal(false)}>
                        No
                    </Button>
                    <Button variant="primary" onClick={handleConfirmStock}>
                        Yes
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default Cart;
