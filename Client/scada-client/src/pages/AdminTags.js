import React, { useState, useEffect } from 'react';
import './AdminTags.css';
import TagInstance from './TagInstance';
import AlarmInstance from './AlarmInstance';
import AdminCreateTag from './AdminCreateTag'; // Import the new component
import { Fab } from '@mui/material';
import { Add } from '@mui/icons-material';
import {CreateAnalog, GetAnalog, GetDigital} from '../services/tagService'
import tagSocketService from '../services/tagSocketService'; 
import alarmSocketService from '../services/alarmSocketService'; 

tagSocketService.startConnection();
alarmSocketService.startConnection();

function AdminTags(props) {
    const { data } = props;
    const digitalTagsWithValues = data.digitalInputs.map(digitalInput => ({
        ...digitalInput,
        value: 0, // Set the initial value here
      }));
      
      // Update analogInputs with the "value" field added
      const analogTagsWithValues = data.analogInputs.map(analogInput => ({
        ...analogInput,
        value: 0, // Set the initial value here
      }));
    const [digitalTags,setDigitalTags] = useState([]);
    const [analogTags,setAnalogTags] = useState([]);
    const [alarmLogs,setAlarmLogs] = useState([]);
    const [isCreateTagDialogOpen, setIsCreateTagDialogOpen] = useState(false);

    useEffect(() => {
        const alarmListener = (newAlarmData) => {
          console.log("DOBIO SAM ALARMMMMMM");
          setAlarmLogs(prevAlarmLogs => [newAlarmData,...prevAlarmLogs]);
        };
      
        alarmSocketService.receiveAlarmData(alarmListener);
        
        updateTagsAndLoad();
    }, []);

    // Rest of your code...

    const loadTags = async ()=>{
        const digitalData = await GetDigital();
        const analogData = await GetAnalog();
        console.log(digitalData);
        console.log(analogData);
        const digitalTagsWithValues = digitalData.map(digitalInput => ({
            ...digitalInput,
            value: 0, // Set the initial value here
          }));
          
          // Update analogInputs with the "value" field added
          const analogTagsWithValues = analogData.map(analogInput => ({
            ...analogInput,
            value: 0, // Set the initial value here
          }));
        setAnalogTags(analogTagsWithValues);
        setDigitalTags(digitalTagsWithValues);
    }

    const updateTagsAndLoad = async () => {
        await loadTags();
    };
    const handleAddTag = async newTag => {
        setDigitalTags(prevDigitalTags => [...prevDigitalTags, newTag]);
        setIsCreateTagDialogOpen(false);
    };
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
    const handleDeleteTag = (tagId, tagType) => {
        if (tagType === 'DIGITAL') {
          setDigitalTags(prevTags => prevTags.filter(tag => tag.id !== tagId));
        } else if (tagType === 'ANALOG') {
          setAnalogTags(prevTags => prevTags.filter(tag => tag.id !== tagId));
        }
      };
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
              {/* Plus button for creating tags */}
              <div style={{ display: 'flex', justifyContent: 'center', marginTop: '16px' }}>
                <Fab color="primary" aria-label="add" onClick={() => setIsCreateTagDialogOpen(true)}>
                  <Add />
                </Fab>
              </div>
              <div className="tags-below-plus-button" style={{display:'flex',flexWrap:'wrap'}}>
                {digitalTags.map(tag => (
                  <TagInstance key={tag.id} {...tag} isAdmin={true} tagType={"DIGITAL"}   onDeleteTag={handleDeleteTag}  onUpdateTags={updateTagsAndLoad}/>
                ))}
                {analogTags.map(tag => (
                  <TagInstance key={tag.id} {...tag} isAdmin={true} tagType={"ANALOG"}   onDeleteTag={handleDeleteTag}  onUpdateTags={updateTagsAndLoad} />
                ))}
              </div>
            </div>
          </div>
          <div className="scrollable-container">
            <div className="logs-container">
              {alarmLogs.map(alarm => (
                <AlarmInstance key={alarm.id} {...alarm} />
              ))}
            </div>
          </div>
          <AdminCreateTag
            isOpen={isCreateTagDialogOpen}
            onClose={() => setIsCreateTagDialogOpen(false)}
            handleAddTag={handleAddTag}
            onUpdateTags={updateTagsAndLoad}
          />
        </div>
      );
}

export default AdminTags;
