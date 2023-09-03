import React, { useState } from 'react';
import './Reports.css';
import AdminHome from './AdminHome';
import ClientHome from './ClientHome';
import AlarmReport from './AlarmReport';
import AnalogDateReport from './AnalogDateReport';
import DigitalDateReport from './DigitalDateReport';
import AnalogLastValueReadReport from './AnalogLastValueReadReport';
import DigitalLastValueReadReport from './DigitalLastValueReadReport';
import AnalogValuesTagReport from './AnalogValuesTagReport';
import DigitalValuesTagReport from './DigitalValuesTagReport';

function Reports(props) {
  const [selectedOption, setSelectedOption] = useState(''); // State to store the selected option

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value); // Update the selected option when the dropdown changes
  };

  return (
    <div className="reports-main-content">
      <div className="select-div">
        <label>Select an option: </label>
        <select value={selectedOption} onChange={handleOptionChange}>
          <option value="">Select an option</option>
          <option value="alarm">Alarms</option>
          <option value="analogDate">Analog Date</option>
          <option value="digitalDate">Digital Date</option>
          <option value="analogValues">Analog Values</option>
          <option value="digitalValues">Digital Values</option>
          <option value="AnalogTagID">Analog Tag</option>
          <option value="DigitalTagID">Digital Tag</option>

        </select>
      </div>
      {selectedOption === 'alarm' && <AlarmReport />}
      {selectedOption === 'analogDate' && <AnalogDateReport />}
      {selectedOption === 'digitalDate' && <DigitalDateReport />}
      {selectedOption === 'analogValues' && <AnalogLastValueReadReport />}
      {selectedOption === 'digitalValues' && <DigitalLastValueReadReport />}
      {selectedOption === 'AnalogTagID' && <AnalogValuesTagReport />}
      {selectedOption === 'DigitalTagID' && <DigitalValuesTagReport />}
    </div>
  );
}

export default Reports;
