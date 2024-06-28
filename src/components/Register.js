import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const Register = () => {
    const [user, setUser] = useState({ username: '', password: '' });
    const [error, setError] = useState(null);
    const [success, setSuccess] = useState(null);
    const [isSubmitting, setIsSubmitting] = useState(false); 
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsSubmitting(true); 
        try {
            await api.register(user);
            setSuccess('Registration successful! Redirecting to login...');
            setError(null);
            setTimeout(() => {
                navigate('/login');
            }, 3000); 
        } catch (err) {
            setError('Registration failed. Please try again.');
            setSuccess(null);
            setIsSubmitting(false); 
            console.error(err);
        }
    };

    return (
        <div className="container mt-5">
            <h1>Register</h1>
            {error && <div className="alert alert-danger">{error}</div>}
            {success && <div className="alert alert-success">{success}</div>}
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Username</label>
                    <input type="text" name="username" value={user.username} onChange={handleChange} className="form-control" required />
                </div>
                <div className="form-group">
                    <label>Password</label>
                    <input type="password" name="password" value={user.password} onChange={handleChange} className="form-control" required />
                </div>
                <button type="submit" className="btn btn-primary" disabled={isSubmitting}>Register</button> {}
            </form>
        </div>
    );
};

export default Register;
