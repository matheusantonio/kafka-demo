// JavaScript source code
db.createUser(
    {
        user: "admin",
        pwd: "admin",
        roles: [
            {
                role: "readWrite",
                db: "kafkaDemo"
            }
        ]
    }
);

db.createCollectino("messages");