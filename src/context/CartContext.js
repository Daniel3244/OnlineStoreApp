import React, { createContext, useContext, useReducer } from 'react';

const CartContext = createContext();

const cartReducer = (state, action) => {
    switch (action.type) {
        case 'ADD_TO_CART':
            const existingProduct = state.find(product => product.id === action.product.id);
            if (existingProduct) {
                return state.map(product =>
                    product.id === action.product.id
                        ? { ...product, quantity: product.quantity + 1 }
                        : product
                );
            } else {
                return [...state, { ...action.product, quantity: 1 }];
            }
        case 'REMOVE_FROM_CART':
            return state.filter(product => product.id !== action.id);
        case 'UPDATE_QUANTITY':
            return state.map(product =>
                product.id === action.id
                    ? { ...product, quantity: action.quantity }
                    : product
            );
        case 'CLEAR_CART':
            return [];
        default:
            return state;
    }
};

export const CartProvider = ({ children }) => {
    const [cart, dispatch] = useReducer(cartReducer, []);

    return (
        <CartContext.Provider value={{ cart, dispatch }}>
            {children}
        </CartContext.Provider>
    );
};

export const useCart = () => {
    return useContext(CartContext);
};
