import { useState } from 'react';

export default function SearchBar({ onSearch }) {
  const [value, setValue] = useState('');

  function handleChange(e) {
    setValue(e.target.value);
    onSearch(e.target.value);
  }

  return (
    <div style={{ marginBottom: '10px' }}>
      <label>Search by name:</label>
      <input
        type="text"
        value={value}
        onChange={handleChange}
        placeholder="Type medicine name..."
      />
    </div>
  );
}
