<?php
header('Access-Control-Allow-Origin:*');
header('Content-Type: application/json');

include_once('../../Config/db.php');
include_once('../../Model/Product.php');
include_once('../../Config/Common.php');

// $_POST = json_decode(file_get_contents('php://input'), true);

$db = new Database();
$product = new Product($db->connect());

/*
    1 Hangout with friends 
    2 Dating
    3 Party
*/

$collectionsID = isset($_GET["collectionsID"]) && !empty($_GET["collectionsID"]) ? $_GET["collectionsID"] : "1";

$arrayProduct = $product->getCollectionsProduct($collectionsID);

$array = [];

$array = array(
    "status" => $SUCCESS,
    "data" => $arrayProduct,
    "message" => "Get product from a collections"
);

echo json_encode($array);
