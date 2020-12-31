import { v4 } from 'uuid';

export interface ICart {
    id: string;
    cartItems: ICartItem[];
}

export interface ICartItem {
    id: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export class Cart implements ICart {
    id = v4();
    cartItems: ICartItem[] = []; // prevent null exception upon instantiation
}

export interface ICartTotals {
    shipping: number;
    subtotal: number;
    total: number;
}
