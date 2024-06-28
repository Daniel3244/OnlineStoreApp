import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const AddProduct = () => {
    const [product, setProduct] = useState({ name: '', price: '', stock: '', description: '' });
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProduct({ ...product, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await api.addProduct(product);
            navigate('/products'); 
        } catch (err) {
            setError('Failed to add product. Please try again.');
        }
    };

    return (
        <div className="container mt-5">
            <h1>Add Product</h1>
            {error && <div className="alert alert-danger">{error}</div>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Product Name</label>
                    <input type="text" name="name" value={product.name} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Price</label>
                    <input type="number" name="price" value={product.price} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Stock</label>
                    <input type="number" name="stock" value={product.stock} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Description</label>
                    <textarea name="description" value={product.description} onChange={handleChange} className="form-control" required />
                </div>
                <button type="submit" className="btn btn-primary">Add Product</button>
            </form>
        </div>
    );
};

export default AddProduct;
