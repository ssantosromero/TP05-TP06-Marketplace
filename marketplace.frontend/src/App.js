import React, { useEffect, useState } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

function App() {
  const [products, setProducts] = useState([]);
  const [cart, setCart] = useState([]);

  // Cargar productos desde el backend
  useEffect(() => {
    axios
      .get("http://localhost:5011/api/Products")
      .then((res) => {
        console.log("Productos desde API:", res.data);
        setProducts(res.data);
      })
      .catch((err) => console.error("Error al cargar productos", err));
  }, []);

  const addToCart = (product) => {
    if (!cart.find((p) => p.id === product.id)) {
      setCart([...cart, product]);
    }
  };

  const removeFromCart = (id) => {
    setCart(cart.filter((p) => p.id !== id));
  };

  const total = cart.reduce((acc, item) => acc + item.price, 0);

  return (
    <div className="app-container">
      <h1 className="text-center mb-5">🛍️ Marketplace</h1>

      <div className="row">
        {products.map((product) => (
          <div className="col-md-3 mb-4" key={product.id}>
            <div className="card shadow border-0 h-100">
              <img
                src={`https://via.placeholder.com/200x150?text=${encodeURIComponent(
                  product.name
                )}`}
                alt={product.name}
                className="card-img-top p-3"
              />
              <div className="card-body text-center">
                <h5 className="card-title">{product.name}</h5>
                <p className="card-text text-muted">${product.price}</p>
                {cart.find((p) => p.id === product.id) ? (
                  <button
                    className="btn btn-danger btn-sm"
                    onClick={() => removeFromCart(product.id)}
                  >
                    Quitar
                  </button>
                ) : (
                  <button
                    className="btn btn-primary btn-sm"
                    onClick={() => addToCart(product)}
                  >
                    Agregar
                  </button>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>

      <div className="cart mt-5 text-center">
        <h3>🛒 Carrito ({cart.length})</h3>
        {cart.length === 0 ? (
          <p>Tu carrito está vacío.</p>
        ) : (
          <>
            <ul className="list-group mb-3">
              {cart.map((item) => (
                <li
                  key={item.id}
                  className="list-group-item d-flex justify-content-between align-items-center"
                >
                  {item.name}
                  <span>${item.price}</span>
                </li>
              ))}
            </ul>
            <h4>Total: ${total.toFixed(2)}</h4>
          </>
        )}
      </div>
    </div>
  );
}

export default App;
