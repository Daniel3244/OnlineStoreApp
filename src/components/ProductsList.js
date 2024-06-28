import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import api from '../services/api';
import { useCart } from '../context/CartContext';
import './ProductsList.css'; 

const ProductsList = () => {
    const [products, setProducts] = useState([]);
    const [error, setError] = useState(null);
    const [showAddedMessage, setShowAddedMessage] = useState(false); 
    const role = localStorage.getItem('role'); 
    const token = localStorage.getItem('token'); 
    const { dispatch } = useCart();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await api.fetchProducts();
                setProducts(response.data);
            } catch (err) {
                setError('Failed to fetch products.');
                console.error(err);
            }
        };

        fetchProducts();
    }, []);

    const handleAddToCart = (product) => {
        if (!token) {
            navigate('/login', { state: { from: { pathname: '/products' } } });
        } else {
            dispatch({ type: 'ADD_TO_CART', product });
            setShowAddedMessage(true);
            setTimeout(() => setShowAddedMessage(false), 3000); 
        }
    };

    const handleDelete = async (id) => {
        try {
            await api.deleteProduct(id);
            setProducts(products.filter(product => product.id !== id));
        } catch (err) {
            setError('Failed to delete product.');
            console.error(err);
        }
    };

    return (
        <div className="container mt-5">
            <h1>Products List</h1>
            {error && <div className="alert alert-danger">{error}</div>}
            {role === 'Admin' && (
                <Link to="/add-product" className="btn btn-primary mb-3">Add Product</Link>
            )}
            <ul className="list-group">
                {products.map(product => (
                    <li key={product.id} className="list-group-item d-flex justify-content-between align-items-center">
                        <div>
                            <span className={product.stock === 0 ? 'product-out-of-stock' : ''}>
                                {product.name} - ${product.price.toFixed(2)}
                            </span>
                            {product.stock === 0 && <span className="out-of-stock-text"> OUT OF STOCK!</span>}
                        </div>
                        <div>
                            <Link to={`/product/${product.id}`} className="btn btn-info btn-sm mr-2">Details</Link>
                            {role === 'Admin' && (
                                <>
                                    <Link to={`/update-product/${product.id}`} className="btn btn-warning btn-sm mr-2">Update</Link>
                                    <button onClick={() => handleDelete(product.id)} className="btn btn-danger btn-sm mr-2">Delete</button>
                                </>
                            )}
                            <button 
                                onClick={() => handleAddToCart(product)} 
                                className="btn btn-success btn-sm"
                                disabled={product.stock === 0}
                            >
                                Add to Cart
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
            {showAddedMessage && <div className="added-to-cart-message">Added to Cart</div>}
        </div>
    );
};

export default ProductsList;
