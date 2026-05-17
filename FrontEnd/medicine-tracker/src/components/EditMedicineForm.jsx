import { useState } from 'react';

export default function EditMedicineForm({ medicine, onUpdate, onCancel }) {
  const [fullName, setFullName] = useState(medicine.fullName);
  const [brand, setBrand] = useState(medicine.brand);
  const [notes, setNotes] = useState(medicine.notes || '');
  const [expiryDate, setExpiryDate] = useState(medicine.expiryDate.split('T')[0]);
  const [quantity, setQuantity] = useState(String(medicine.quantity));
  const [price, setPrice] = useState(String(medicine.price));
  const [error, setError] = useState('');

  async function handleSubmit(e) {
    e.preventDefault();

    if (!fullName) { setError('Full name is required'); return; }
    if (!brand) { setError('Brand is required'); return; }
    if (!expiryDate) { setError('Expiry date is required'); return; }
    if (!price || Number(price) <= 0) { setError('Price must be greater than 0'); return; }
    if (quantity === '' || Number(quantity) < 0) { setError('Quantity must be 0 or more'); return; }

    setError('');
    try {
      await onUpdate(medicine.id, {
        fullName,
        brand,
        notes,
        expiryDate: new Date(expiryDate).toISOString(),
        quantity: Number(quantity),
        price: Number(price),
      });
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div className="section">
      <h2>Edit Medicine</h2>
      {error && <p className="error">{error}</p>}
      <form onSubmit={handleSubmit}>
        <label>Full Name</label>
        <input value={fullName} onChange={e => setFullName(e.target.value)} />

        <label>Brand</label>
        <input value={brand} onChange={e => setBrand(e.target.value)} />

        <label>Price</label>
        <input type="number" value={price} onChange={e => setPrice(e.target.value)} />

        <label>Quantity</label>
        <input type="number" value={quantity} onChange={e => setQuantity(e.target.value)} />

        <label>Expiry Date</label>
        <input type="date" value={expiryDate} onChange={e => setExpiryDate(e.target.value)} />

        <label>Notes</label>
        <input value={notes} onChange={e => setNotes(e.target.value)} />

        <button type="submit">Save</button>
        <button type="button" onClick={onCancel}>Cancel</button>
      </form>
    </div>
  );
}
