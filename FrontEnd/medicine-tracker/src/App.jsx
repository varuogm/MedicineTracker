import { useState, useEffect } from 'react';
import Header from './components/Header';
import SearchBar from './components/SearchBar';
import MedicineTable from './components/MedicineTable';
import AddMedicineForm from './components/AddMedicineForm';
import EditMedicineForm from './components/EditMedicineForm';
import SaleForm from './components/SaleForm';
import { getMedicines, addMedicine, updateMedicine, deleteMedicine, getSales, addSale } from './services/medicineApi';
import './styles/app.css';

export default function App() {
  const [activeTab, setActiveTab] = useState('medicines');

  const [medicines, setMedicines] = useState([]);
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState('');

  const [sales, setSales] = useState([]);
  const [salesLoading, setSalesLoading] = useState(false);

  const [showAdd, setShowAdd] = useState(false);
  const [editMedicine, setEditMedicine] = useState(null);
  const [sellMedicine, setSellMedicine] = useState(null);

  function showMsg(msg) {
    setMessage(msg);
    setTimeout(() => setMessage(''), 3000);
  }

  async function loadMedicines(search) {
    setLoading(true);
    try {
      const data = await getMedicines(search);
      setMedicines(data);
    } catch (err) {
      alert('Error while loading medicines: ' + err.message);
    }
    setLoading(false);
  }

  async function loadSales() {
    setSalesLoading(true);
    try {
      const data = await getSales();
      setSales(data);
    } catch (err) {
      alert('Error while loading sales: ' + err.message);
    }
    setSalesLoading(false);
  }

  useEffect(() => {
    loadMedicines('');
  }, []);

  useEffect(() => {
    if (activeTab === 'sales') loadSales();
  }, [activeTab]);

  async function handleAdd(data) {
    await addMedicine(data);
    setShowAdd(false);
    showMsg('Medicine added.');
    loadMedicines('');
  }

  async function handleUpdate(id, data) {
    await updateMedicine(id, data);
    setEditMedicine(null);
    showMsg('Medicine updated.');
    loadMedicines('');
  }

  async function handleDelete(id) {
    if (!window.confirm('Delete this medicine?')) return;
    await deleteMedicine(id);
    showMsg('Medicine deleted.');
    loadMedicines('');
  }

  async function handleSell(data) {
    await addSale(data);
    setSellMedicine(null);
    showMsg('Sale recorded.');
    loadMedicines('');
  }

  return (
    <div>
      <Header activeTab={activeTab} onTabChange={setActiveTab} />

      {message && <p className="success">{message}</p>}

      {activeTab === 'medicines' && (
        <div>
          <SearchBar onSearch={(q) => loadMedicines(q)} />

          {!showAdd && !editMedicine && !sellMedicine && (
            <button onClick={() => setShowAdd(true)}>Add Medicine</button>
          )}

          {showAdd && (
            <AddMedicineForm
              onAdd={handleAdd}
              onCancel={() => setShowAdd(false)}
            />
          )}

          {editMedicine && (
            <EditMedicineForm
              medicine={editMedicine}
              onUpdate={handleUpdate}
              onCancel={() => setEditMedicine(null)}
            />
          )}

          {sellMedicine && (
            <SaleForm
              medicine={sellMedicine}
              onSell={handleSell}
              onCancel={() => setSellMedicine(null)}
            />
          )}

          {!showAdd && !editMedicine && !sellMedicine && (
            <MedicineTable
              medicines={medicines}
              loading={loading}
              onEdit={(m) => setEditMedicine(m)}
              onDelete={handleDelete}
              onSell={(m) => setSellMedicine(m)}
            />
          )}
        </div>
      )}

      {activeTab === 'sales' && (
        <div>
          <h2>Sales Records</h2>
          {salesLoading && <p>Loading...</p>}
          {!salesLoading && sales.length === 0 && <p>No sales yet.</p>}
          {!salesLoading && sales.length > 0 && (
            <table>
              <thead>
                <tr>
                  <th>#</th>
                  <th>Medicine ID</th>
                  <th>Qty Sold</th>
                  <th>Total</th>
                  <th>Date</th>
                </tr>
              </thead>
              <tbody>
                {sales.map((s, i) => (
                  <tr key={s.id}>
                    <td>{i + 1}</td>
                    <td>{s.medicineId}</td>
                    <td>{s.quantitySold}</td>
                    <td>Rs. {s.totalPrice}</td>
                    <td>{new Date(s.saleDate).toLocaleDateString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}
        </div>
      )}
    </div>
  );
}
