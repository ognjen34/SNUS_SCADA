import React, { useState, useEffect } from 'react';
import './ClientHome.css';
import TagInstance from './TagInstance';
import AlarmInstance from './AlarmInstance';

import tagSocketService from '../services/tagSocketService'; 
import alarmSocketService from '../services/alarmSocketService'; 

tagSocketService.startConnection();
alarmSocketService.startConnection();
function ClientHome(props) {
    const { data } = props;
    console.log(data);
    const digitalTagsWithValues = data.digitalInputs.map(digitalInput => ({
        ...digitalInput,
        value: 0, // Set the initial value here
      }));
      
      // Update analogInputs with the "value" field added
      const analogTagsWithValues = data.analogInputs.map(analogInput => ({
        ...analogInput,
        value: 0, // Set the initial value here
      }));
    const [digitalTags,setDigitalTags] = useState(digitalTagsWithValues);
    const [analogTags,setAnalogTags] = useState(analogTagsWithValues);
    const [alarmLogs,setAlarmLogs] = useState([]);

    useEffect(() => {
        const alarmListener = (newAlarmData) => {
          console.log("DOBIO SAM ALARMMMMMM");
          setAlarmLogs(prevAlarmLogs => [newAlarmData,...prevAlarmLogs]);
        };
      
        alarmSocketService.receiveAlarmData(alarmListener);
      
        return () => {
          // Clean up the event listener when the component unmounts
          alarmSocketService.removeAlarmDataListener(alarmListener);
        };
      }, []);
    tagSocketService.receiveAnalogData((newAnalogData) => {
        setAnalogTags(prevAnalogTags => {
            return prevAnalogTags.map(analogTag => {
                if (analogTag.id === newAnalogData.tagId) {
                    return {
                        ...analogTag,
                        value: newAnalogData.value,
                    };
                }
                return analogTag;
            });
        });
    });
    
    tagSocketService.receiveDigitalData((newDigitalData) => {
        setDigitalTags(prevDigitalTags => {
            return prevDigitalTags.map(digitalTag => {
                if (digitalTag.id === newDigitalData.tagId) {
                    return {
                        ...digitalTag,
                        value: newDigitalData.value,
                    };
                }
                return digitalTag;
            });
        });
    });

    return (
        <div className="client-container">
          <div className="scrollable-container">
            <div className="tag-container">
              {digitalTags.map(tag => (
                <TagInstance key={tag.id} {...tag} />
              ))}
              {analogTags.map(tag => (
                <TagInstance key={tag.id} {...tag} />
              ))}
            </div>
          </div>
          <div className="scrollable-container">
            <div className="logs-container">
              {alarmLogs.map(alarm => (
                <AlarmInstance key={alarm.id} {...alarm} />
              ))}
            </div>
          </div>
        </div>
      );
}

export default ClientHome;
