<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Cart.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$cart = new Cart($db->connect());

$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$productID = isset($_POST["productID"]) && !empty($_POST["productID"]) ? $_POST["productID"] : "";
$size = isset($_POST["size"]) && !empty($_POST["size"]) ? $_POST["size"] : "";
$quantity = isset($_POST["quantity"]) && !empty($_POST["quantity"]) ? $_POST["quantity"] : "";

$status = $cart->InsertCart($userID, $productID, $size, $quantity);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Add to cart successfully!"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Add to cart fail!"
    );
}


echo json_encode($array);
