import { useState } from 'react';

export default function SaleForm({ medicine, onSell, onCancel }) {
  const [quantity, setQuantity] = useState('1');
  const [error, setError] = useState('');

  async function handleSubmit(e) {
    e.preventDefault();

    const qty = Number(quantity);
    if (!qty || qty <= 0) 
      { setError('Quantity must be at least 1'); 
        return;

       }
    if (qty > medicine.quantity) 
      { 
        setError(`Only ${medicine.quantity} units available`);
         return; 
        }

    setError('');
    try {
      await onSell({ medicineId: medicine.id, quantitySold: qty });
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div className="section">
      <h2>Sell Medicine</h2>
      <p>Medicine: {medicine.fullName}</p>
      <p>Available stock: {medicine.quantity}</p>
      <p>Price per unit: Rs. {medicine.price}</p>

      {error && <p className="error">{error}</p>}

      <form onSubmit={handleSubmit}>
        <label>Quantity to Sell</label>
        <input
          type="number"
          min="1"
          max={medicine.quantity}
          value={quantity}
          onChange={e => setQuantity(e.target.value)}
        />

        <p>Total: Rs. {(Number(quantity) * medicine.price).toFixed(2)}</p>

        <button type="submit">Confirm Sale</button>
        <button type="button" onClick={onCancel}>Cancel</button>
      </form>
    </div>
  );
}
