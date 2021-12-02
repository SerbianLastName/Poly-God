// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts/token/ERC1155/presets/ERC1155PresetMinterPauser.sol";

contract BuySingleLandPlot is ERC1155PresetMinterPauser {
    uint32 public plotIDCounter;
    address payable public wallet;
    uint256 public price;

    mapping(uint32 => uint32) public idmap;
    mapping(uint256 => uint32) public lookupmap;

    constructor(address payable _wallet)
        ERC1155PresetMinterPauser("Land Plot - ")
    {
        wallet = _wallet;
        price = 10**18;
    }

    function addPlotID() public payable virtual {
        // Prevent 32 bit overflow, limit land plots to 2147483646

        require(msg.value >= price, "Not enough Matic sent!");
        if (plotIDCounter + 1 <= 2147483646) {
            uint32 plotID = plotIDCounter;
            idmap[plotID] = plotIDCounter;
            lookupmap[plotIDCounter] = plotID;
            _mint(msg.sender, plotIDCounter, 1, "");
            plotIDCounter = plotIDCounter + 1;
            wallet.transfer(msg.value);
        }
    }

    function uri(uint32 id) public view virtual returns (uint32) {
        return uint32(lookupmap[id]);
    }

    function changePrice(uint256 newPrice) public virtual {
        require(msg.sender == wallet);
        price = newPrice;
    }

    function getPlotIDs(address account)
        public
        view
        returns (uint256[] memory)
    {
        uint256 numTokens = 0;
        for (uint256 i = 0; i <= plotIDCounter; i++) {
            if (balanceOf(account, i) > 0) {
                numTokens++;
            }
        }

        uint256[] memory ret = new uint256[](numTokens);
        uint256 counter = 0;
        for (uint256 i = 0; i <= plotIDCounter; i++) {
            if (balanceOf(account, i) > 0) {
                ret[counter] = i;
                counter++;
            }
        }

        return ret;
    }
}
