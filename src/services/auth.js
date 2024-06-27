import axios from 'axios';

const AUTH_API_BASE_URL = 'http://localhost:5166/api'; // Adjust this URL to match your API

export const authenticate = async (username, password) => {
    try {
        const response = await axios.post(`${AUTH_API_BASE_URL}/authenticate`, { username, password });
        const token = response.data.token;
        localStorage.setItem('token', token);
        return { token };
    } catch (error) {
        throw new Error('Failed to authenticate');
    }
};

export const register = async (user) => {
    try {
        const response = await axios.post(`${AUTH_API_BASE_URL}/register`, user);
        return response.data;
    } catch (error) {
        throw new Error('Failed to register');
    }
};
