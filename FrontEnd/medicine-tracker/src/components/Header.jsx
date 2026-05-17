export default function Header({ activeTab, onTabChange }) {
  return (
    <div>
      <h1>Medicine Tracker</h1>
      <nav>
        <button
          className={activeTab === 'medicines' ? 'active' : ''}
          onClick={() => onTabChange('medicines')}
        >
          Medicines
        </button>
        <button
          className={activeTab === 'sales' ? 'active' : ''}
          onClick={() => onTabChange('sales')}
        >
          Sales
        </button>
      </nav>
    </div>
  );
}
