import axios from 'axios';

const API_BASE_URL = 'http://localhost:5000/api/webapi'; // Adjust this URL to match your API base URL

const api = axios.create({
  baseURL: API_BASE_URL,
});

// Interceptor to add the Authorization header to each request
api.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

export const fetchProducts = () => api.get('/Product');
export const fetchProductById = (id) => api.get(`/Product/${id}`);
export const authenticate = (credentials) => api.post('/User/login', credentials);
export const register = (user) => api.post('/User/register', user);
export const addProduct = (product) => api.post('/Product', product);
export const updateProduct = (id, product) => api.put(`/Product/${id}`, product); // Add this line
export const deleteProduct = (id) => api.delete(`/Product/${id}`);
export const createOrder = (order) => api.post('/Order', order);

export default { fetchProducts, fetchProductById, authenticate, register, addProduct, updateProduct, deleteProduct, createOrder };
