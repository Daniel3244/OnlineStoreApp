import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import api from '../services/api';
import './UpdateProduct.css'; 

const UpdateProduct = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [product, setProduct] = useState({
        name: '',
        description: '',
        price: '',
        stock: ''
    });
    const [error, setError] = useState(null);
    const [showUpdatedMessage, setShowUpdatedMessage] = useState(false); 

    useEffect(() => {
        const fetchProduct = async () => {
            try {
                const response = await api.fetchProductById(id);
                setProduct(response.data);
            } catch (err) {
                setError('Failed to fetch product details.');
                console.error(err);
            }
        };

        fetchProduct();
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProduct({ ...product, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.updateProduct(id, product);
            setShowUpdatedMessage(true);
            setTimeout(() => {
                setShowUpdatedMessage(false);
                navigate('/products');
            }, 1000); 
        } catch (err) {
            setError('Failed to update product.');
            console.error(err);
        }
    };

    return (
        <div className="container mt-5">
            <h1>Update Product</h1>
            {error && <div className="alert alert-danger">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Name</label>
                    <input type="text" name="name" value={product.name} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea name="description" value={product.description} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Price</label>
                    <input type="number" name="price" value={product.price} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Stock</label>
                    <input type="number" name="stock" value={product.stock} onChange={handleChange} className="form-control" required />
                </div>
                <button type="submit" className="btn btn-primary">Update Product</button>
            </form>
            {showUpdatedMessage && <div className="updated-message">Product Updated Successfully</div>}
        </div>
    );
};

export default UpdateProduct;
