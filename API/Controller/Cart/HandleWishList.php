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

$status = $cart->HandleWishList($userID, $productID);

$array = [];

if ($status == 1 || $status == 3) {
    $array = array(
        "status" => $SUCCESS,
        // "data" => array(),
        "message" => "$status",
    );
} else {
    $array = array(
        "status" => $FAIL,
        // "data" => array(),
        "message" => "Add and delete product in Wishlist fail",
    );
}

echo json_encode($array);
