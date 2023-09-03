import { HubConnectionBuilder, HubConnection } from "@microsoft/signalr";



const AlarmSocketService = {
    connection: null,

    startConnection: () => {
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5045/Hub/alarm", { withCredentials: true })
            .build();

        connection.start().catch((error) => {
            console.error("SignalR connection error: ", error);
        });

        AlarmSocketService.connection = connection;
    },

    receiveAlarmData: (callback) => {
        AlarmSocketService.connection?.on("ReceiveAlarmData", (data) => {
            callback(data);
        });
    },
};

export default AlarmSocketService;