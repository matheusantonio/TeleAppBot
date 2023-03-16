// JavaScript source code
db.createUser(
    {
        user: "usrTeleAppBot",
        pwd: "pwdTeleAppBot",
        roles: [
            {
                role: "readWrite",
                db: "teleAppBot"
            }
        ]
    }
);

db.createCollection("Mensagens");
db.createCollection("Conversas");
db.createCollection("Contatos");