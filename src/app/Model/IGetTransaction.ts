export interface IGetTransaction {
    transactionId: number,
    senderName: string,
    receiverName: string,
    amount: number,
    timestamp : Date,
    type : string,
    senderId : number
}