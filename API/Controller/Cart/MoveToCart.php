<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Cart.php');
include_once('../../Config/Common.php');

$_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$cart = new Cart($db->connect());

$cartID = isset($_POST["cartID"]) && !empty($_POST["cartID"]) ? $_POST["cartID"] : "";
$userID = isset($_POST["userID"]) && !empty($_POST["userID"]) ? $_POST["userID"] : "";
$productID = isset($_POST["productID"]) && !empty($_POST["productID"]) ? $_POST["productID"] : "";
$size = isset($_POST["size"]) && !empty($_POST["size"]) ? $_POST["size"] : "";

$status = $cart->MoveToCart($cartID, $userID, $productID, $size);

$array = [];

if ($status == 1) {
    $array = array(
        "status" => $SUCCESS,
        "message" => "Move to cart successfully!"
    );
} else {
    $array = array(
        "status" => $FAIL,
        "message" => "Move cart fail!"
    );
}

echo json_encode($array);
