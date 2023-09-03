import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";



const TagSocketService = {
    connection: null,

    startConnection: () => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5045/Hub/tag", { withCredentials: true })
            .build();

        connection.start().catch((error) => {
            console.error("SignalR connection error: ", error);
        });

        TagSocketService.connection = connection;
    },

    receiveAnalogData: (callback) => {
        TagSocketService.connection?.on("ReceiveAnalogData", (data) => {
            callback(data);
        });
    },

    receiveDigitalData: (callback) => {
        TagSocketService.connection?.on("ReceiveDigitalData", (data) => {
            callback(data);
        });
    },
};

export default TagSocketService;