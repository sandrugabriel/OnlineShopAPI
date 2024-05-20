INSERT INTO products VALUES (1,'s24',7000,'phone',"2024-05-11T18:49:00",10);
INSERT INTO products VALUES (2,'laptop asus g15',5000,'laptop',"2024-05-11T18:49:00",5);
INSERT INTO products VALUES (3,'tv samsung 4k',6000,'television',"2024-05-11T18:49:00",7);

INSERT INTO orders VALUES (1,1,7000,'sadu',"2024-05-11T18:49:00",'open');
INSERT INTO orders VALUES (2,1,5000,'test',"2024-05-11T18:49:00",'closed');
INSERT INTO orders VALUES (3,2,6000,'test1',"2024-05-11T18:49:00",'closed');
INSERT INTO orders VALUES (4,3,24000,'sadu',"2024-05-11T18:49:00",'open');

INSERT INTO customers VALUES (1,'gabi@gamil.com','gabi1234','gabi',"07777777",'sadu','romania');
INSERT INTO customers VALUES (2,'filip@gamil.com','1234','filip',"07777737",'sadu','romania');
INSERT INTO customers VALUES (3,'test@gamil.com','test1234','test',"07777777",'siibu','romania');

INSERT INTO orderdetails VALUES (1,1,1,7000,1);
INSERT INTO orderdetails VALUES (2,2,2,5000,1);
INSERT INTO orderdetails VALUES (3,3,3,6000,1);
INSERT INTO orderdetails VALUES (4,4,2,10000,2);
INSERT INTO orderdetails VALUES (5,4,1,14000,2);

INSERT INTO options VALUES (1,'with 1tb',250);
INSERT INTO options VALUES (2,'with 256gb',150);
INSERT INTO options VALUES (3,'with case',100);
INSERT INTO options VALUES (4,'with 128gb',0);
INSERT INTO options VALUES (5,'with stand',80);
INSERT INTO options VALUES (6,'with 16gb ram',300);

INSERT INTO productoptions VALUES (1,1,1);
INSERT INTO productoptions VALUES (2,1,2);
INSERT INTO productoptions VALUES (3,1,3);
INSERT INTO productoptions VALUES (4,1,4);
INSERT INTO productoptions VALUES (5,2,1);
INSERT INTO productoptions VALUES (6,2,6);
INSERT INTO productoptions VALUES (7,3,5);

/*
            Create.Table("sendorders")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CustomerId").AsInt32().NotNullable()
                .WithColumn("OrderId").AsInt32().NotNullable()
                .WithColumn("DateSendOrder").AsDateTime().NotNullable()
                .WithColumn("CardPayment").AsBoolean().NotNullable()
                .WithColumn("NumberCard").AsString().Nullable()
                .WithColumn("CvvNumber").AsInt32().Nullable()
                .WithColumn("DateExpirated").AsDate().Nullable();
*/
