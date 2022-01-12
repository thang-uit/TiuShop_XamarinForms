<?php
class Cart
{
    public $conn;

    public $cartID = "cartID";
    public $productID = "productID";
    public $name = "name";
    public $image = "image";
    public $price = "price";
    public $sale = "sale";
    public $isSale = "isSale";
    public $finalPrice = "finalPrice";
    public $size = "size";
    public $quantity = "quantity";

    public function __construct($database)
    {
        $this->conn = $database;
    }
    
    public function GetCart($type, $userID)
    {
        $arrayProductCart = [];

        $query = "SELECT * FROM `cart`, `product` WHERE `cart`.`Pro_ID` = `product`.`Pro_ID` AND `cart`.`Use_ID` = '$userID' AND `cart`.`Car_Type` = $type;";
        $stmt = $this->conn->prepare($query);
        $stmt->setFetchMode(PDO::FETCH_ASSOC);
        $stmt->execute();

        while ($row = $stmt->fetch()) {
            $query1 = "SELECT `productimg`.`Pim_Img` FROM `product`, `productimg` WHERE `product`.`Pro_ID` = `productimg`.`Pro_ID` AND `product`.`Pro_ID` = '" . $row["Pro_ID"] . "';";
            $stmt1 = $this->conn->prepare($query1);
            $stmt1->execute();
            $row1 = $stmt1->fetch(PDO::FETCH_ASSOC);

            array_push($arrayProductCart, array(
                $this->cartID =>  $row["Car_ID"],
                $this->productID => $row["Pro_ID"],
                $this->name => $row["Pro_Name"],
                $this->image => array($row1["Pim_Img"]),
                $this->price => number_format($row['Pro_Price'], 0, ",", "."),
                $this->sale => $row["Pro_Sale"] . "%",
                $this->isSale => $row["Pro_Sale"] > 0 ? "True" : "False",
                $this->finalPrice => number_format($this->caculateFinalPrice($row['Pro_Price'], $row["Pro_Sale"]), 0, ",", "."),
                $this->size => $row["Pro_Size"],
                $this->quantity => (int) $row["Car_Amount"]
            ));
        }

        return $arrayProductCart;
    }

    public function InsertCart($userID, $productID, $size, $quantity)
    {
        $query = "SELECT * FROM `cart` WHERE `Use_ID` = '$userID' AND `Pro_ID` = '$productID' AND `Pro_Size` = '$size' AND `cart`.`Car_Type` = 1;";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        if ($stmt->rowCount() <= 0) {
            $queryInsert = "INSERT INTO `cart` (`Car_ID`, `Use_ID`, `Pro_ID`, `Pro_Size`, `Car_Amount`, `Car_Type`) VALUES (NULL, ?, ?, ?, ?, 1);";
            $data = array($userID, $productID, $size, $quantity);
            $stmt1 = $this->conn->prepare($queryInsert);
            $stmt1->execute($data);

            if ($stmt1) {
                return 1; // Success
            } else {
                return 2; // Fail
            }
        } else {
            $row = $stmt->fetch(PDO::FETCH_ASSOC);

            $newQuantity = $row['Car_Amount'] + $quantity;

            $queryUpdate = "UPDATE `cart` SET `Car_Amount` = ? WHERE `cart`.`Use_ID` = ? AND `cart`.`Pro_ID` = ? AND `cart`.`Pro_Size` = ? AND `cart`.`Car_Type` = 1;";
            $data = array($newQuantity, $userID, $productID, $size);
            $stmt2 = $this->conn->prepare($queryUpdate);
            $stmt2->execute($data);

            if ($stmt2) {
                return 1; // Success
            } else {
                return 2; // Fail
            }
        }
    }

    public function HandleWishList($userID, $productID)
    {
        $query = "SELECT * FROM `cart` WHERE `Use_ID` = '$userID' AND `Pro_ID` = '$productID' AND `Car_Type` = 0;";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        if ($stmt->rowCount() <= 0) {
            $queryInsert = "INSERT INTO `cart` (`Car_ID`, `Use_ID`, `Pro_ID`, `Pro_Size`, `Car_Amount`, `Car_Type`) VALUES (NULL, ?, ?, 'XL', 1, 0);";
            $data = array($userID, $productID);
            $stmt1 = $this->conn->prepare($queryInsert);
            $stmt1->execute($data);

            if ($stmt1) {
                return 1; // Insert Success
            } else {
                return 2; // Insert Fail
            }
        } else {
            $queryDelete = "DELETE FROM `cart` WHERE `Use_ID` = ? AND `Pro_ID` = ? AND `Car_Type` = 0;";
            $data1 = array($userID, $productID);
            $stmt2 = $this->conn->prepare($queryDelete);
            $stmt2->execute($data1);

            if ($stmt2) {
                return 3; // Delete Success
            } else {
                return 4; // Delete Fail
            }
        }
    }

    public function UpdateCart($cartID, $quantity)
    {
        $queryUpdate = "UPDATE `cart` SET `Car_Amount` = ? WHERE `cart`.`Car_ID` = ? AND `cart`.`Car_Type` = 1;";
        $data = array($quantity, $cartID);
        $stmt = $this->conn->prepare($queryUpdate);
        $stmt->execute($data);

        if ($stmt) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }

    public function DeleteCart($cartID)
    {
        $queryDelete = "DELETE FROM `cart` WHERE `cart`.`Car_ID` = ?;";
        $data = array($cartID);
        $stmt = $this->conn->prepare($queryDelete);
        $stmt->execute($data);

        if ($stmt) {
            return 1; // Success
        } else {
            return 2; // Fail
        }
    }

    public function MoveToCart($cartID, $userID, $productID, $size)
    {
        $query = "SELECT * FROM `cart` WHERE `Use_ID` = '$userID' AND `Pro_ID` = '$productID' AND `Pro_Size` = '$size' AND `cart`.`Car_Type` = 1;";
        $stmt = $this->conn->prepare($query);
        $stmt->execute();

        if ($stmt->rowCount() > 0) {
            $row = $stmt->fetch(PDO::FETCH_ASSOC);
            $newQuantity = $row['Car_Amount'] + 1;

            $queryUpdate = "UPDATE `cart` SET `Car_Amount` = ? WHERE `cart`.`Use_ID` = ? AND `cart`.`Pro_ID` = ? AND `cart`.`Pro_Size` = ? AND `cart`.`Car_Type` = 1;";
            $data = array($newQuantity, $userID, $productID, $size);
            $stmt1 = $this->conn->prepare($queryUpdate);
            $stmt1->execute($data);

            if ($stmt1) {
                $queryDelete = "DELETE FROM `cart` WHERE `cart`.`Car_ID` = ?;";
                $data1 = array($cartID);
                $stmt2 = $this->conn->prepare($queryDelete);
                $stmt2->execute($data1);

                if ($stmt2) {
                    return 1; // Success
                } else {
                    return 2; // Fail
                }
            } else {
                return 2; // Fail
            }
        } else {
            $queryUpdate1 = "UPDATE `cart` SET `Car_Type` = 1 WHERE `cart`.`Car_ID` = ?;";
            $data2 = array($cartID);
            $stmt3 = $this->conn->prepare($queryUpdate1);
            $stmt3->execute($data2);

            if ($stmt3) {
                return 1; // Success
            } else {
                return 2; // Fail
            }
        }
    }

    private function caculateFinalPrice($price, $sale)
    {
        if ($sale != 0) {
            return $price * (100 - $sale) / 100;
        }
        return $price;
    }
}
