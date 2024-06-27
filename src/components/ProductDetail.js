import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Modal, Button } from 'react-bootstrap';
import { fetchProductById } from '../services/api';
import { useCart } from '../context/CartContext';

const ProductDetail = () => {
    const { id } = useParams();
    const [product, setProduct] = useState(null);
    const [error, setError] = useState(null);
    const { dispatch } = useCart();
    const navigate = useNavigate();
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        const loadProduct = async () => {
            try {
                const response = await fetchProductById(id);
                setProduct(response.data);
            } catch (err) {
                setError(err.message);
            }
        };

        loadProduct();
    }, [id]);

    const handleAddToCart = () => {
        dispatch({ type: 'ADD_TO_CART', product });
        setShowModal(true);
    };

    const handleContinueShopping = () => {
        setShowModal(false);
        navigate('/products');
    };

    const handleGoToCart = () => {
        setShowModal(false);
        navigate('/cart');
    };

    if (error) {
        return <div className="container mt-5"><div className="alert alert-danger">{error}</div></div>;
    }

    if (!product) {
        return <div className="container mt-5"><div className="spinner-border" role="status"><span className="sr-only">Loading...</span></div></div>;
    }

    return (
        <div className="container mt-5">
            <h1>Product Details</h1>
            <ul className="list-group">
                <li className="list-group-item">
                    <strong>Name:</strong> {product.name}
                </li>
                <li className="list-group-item">
                    <strong>Description:</strong> {product.description}
                </li>
                <li className="list-group-item">
                    <strong>Price:</strong> ${product.price.toFixed(2)}
                </li>
                <li className="list-group-item">
                    <strong>Stock:</strong> {product.stock}
                </li>
            </ul>
            <button onClick={handleAddToCart} className="btn btn-primary mt-3" disabled={product.stock === 0}>{product.stock === 0 ? 'OUT OF STOCK' : 'Add to cart'}</button>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Product Added to Cart</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>{product.name} has been added to your cart.</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleContinueShopping}>
                        Continue Shopping
                    </Button>
                    <Button 
                        variant="primary" 
                        onClick={handleGoToCart} 
                        disabled={product.stock === 0}
                    >
                        {product.stock === 0 ? 'OUT OF STOCK' : 'Go to Cart'}
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default ProductDetail;
