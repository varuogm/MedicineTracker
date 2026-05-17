export default function MedicineTable({ medicines, loading, onEdit, onDelete, onSell }) {
  
  if (loading) 
    return <p>Loading...</p>;
  if (medicines.length === 0) 
    return <p>No medicines found.</p>;

  function getRowClass(medicine) {
    const today = new Date();
    const expiry = new Date(medicine.expiryDate);
    const days = Math.ceil((expiry - today) / (1000 * 60 * 60 * 24));
    if (days <= 30) 
      return 'expiring';
    if (medicine.quantity < 10) 
      return 'low-stock';
    return '';
  }

  function formatDate(dateStr) {
    return new Date(dateStr).toLocaleDateString();
  }

  return (
    <div>
      <p>
        <span style={{ marginRight: '16px' }}>
          <span style={{ background: '#b5575f', padding: '2px 8px', border: '1px solid #ccc' }}>Red</span> = expiring within 30 days
        </span>
        <span>
          <span style={{ background: '#eabd28', padding: '2px 8px', border: '1px solid #ccc' }}>Yellow</span> = stock below 10
        </span>
      </p>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Full Name</th>
            <th>Brand</th>
            <th>Expiry Date</th>
            <th>Quantity</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {medicines.map((m, i) => (
            <tr key={m.id} className={getRowClass(m)}>
              <td>{i + 1}</td>
              <td>{m.fullName}</td>
              <td>{m.brand}</td>
              <td>{formatDate(m.expiryDate)}</td>
              <td>{m.quantity}</td>
              <td>Rs. {m.price}</td>
              <td>
                <button onClick={() => onEdit(m)}>Edit</button>
                <button onClick={() => onSell(m)}>Sell</button>
                <button onClick={() => onDelete(m.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
