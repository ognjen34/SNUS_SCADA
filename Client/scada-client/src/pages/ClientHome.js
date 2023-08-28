import React, { useState, useEffect } from 'react';
import './Login.css';
import TagInstance from './TagInstance';
import tagSocketService from '../services/tagSocketService'; 
tagSocketService.startConnection();

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

    const cardDataList = [
        { id: 1, label: 'Card 1 Label', value: '42' },
        { id: 2, label: 'Card 2 Label', value: '23' },
        // Add more card data as needed
    ];
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
        <div className="client-container" style={{ backgroundColor: 'red' }}>
            {digitalTags.map(tag => (
                <TagInstance key={tag.id} {...tag} />
            ))}
            {analogTags.map(tag=>(
                <TagInstance key={tag.id} {...tag}/>
            ))}
        </div>
    );
}

export default ClientHome;
